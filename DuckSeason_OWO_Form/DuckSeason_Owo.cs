using DuckSeason_bHaptics.Owo;
using OWOGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuckSeason_OWO_Form
{
    internal static class DuckSeason_Owo
    {
        public static Dictionary<DSSensation, BakedSensation> OwoSensations = new Dictionary<DSSensation, BakedSensation>();
        public static ConnectionState OwoConnectionState = ConnectionState.Disconnected;

        static Form1 form = null;
        static Dictionary<int, BakedSensation> sensations = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Start the main loop
            Task.Run(() =>
            {
                while (true)
                {
                    // yield to other processes
                    Task.Yield();

                    if (form == null)
                    {
                        // Skip, if form is null
                        continue;
                    }

                    if (sensations == null && form.Created)
                    {
                        // Register all sensations
                        sensations = new Dictionary<int, BakedSensation>();
                        RegisterSensations();
                    }

                    // Call looping function
                    Update();
                }
            });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form = new Form1());
        }

        /// <summary>
        /// Main loop
        /// </summary>
        private static void Update()
        {
            if (OwoConnectionState != OWO.ConnectionState)
            {
                // Log update in connection state
                form.WriteLog("OWO connection state has changed from " + OwoConnectionState + " to " + OWO.ConnectionState);

                // Update connection state
                OwoConnectionState = OWO.ConnectionState;
                form.UpdateConnStateText(OwoConnectionState);
            }

            ReadSensationsToPlay();


            //form.WriteLog("Connection state has changed from " + owoConnectionState + " to " + OWO.ConnectionState);
        }


        private static string lastSenseCommand = "";
        private static long lastSenseExecuted = 0;
        private static long lastRead = 0;
        /// <summary>
        /// Reads the last sensation to play from the ml mod output file
        /// </summary>
        public static void ReadSensationsToPlay()
        {
            if (OwoConnectionState != ConnectionState.Connected)
                // owoskin is not connected
                return;

            if(lastRead + 50000 > DateTime.Now.ToFileTimeUtc())
            {
                // Only read the file every 50ms
                return;
            }

            // Update last read time
            lastRead = DateTime.Now.ToFileTimeUtc();

            var senseFilePath = form.GameDirectory.FullName + "\\Mods\\owo\\playsense.log";

            try
            {
                if (DateTime.Now.Subtract(File.GetLastWriteTime(senseFilePath)).TotalMilliseconds < 5000)
                {
                    // Last write was within 5 seconds

                    var senseLines = File.ReadAllLines(senseFilePath);

                    // Last line
                    var lastSenseMessage = senseLines[senseLines.Length - 1];

                    // When the sense was sent, in file time
                    var lastSenseTimeStr = lastSenseMessage.Split('[', ']')[1];
                    var lastSenseTime = long.Parse(lastSenseTimeStr);

                    // Sense information
                    var playSense = lastSenseMessage.Substring(lastSenseTimeStr.Length + 3);
                    var senseInfo = playSense.Split(new string[] { ";;" }, StringSplitOptions.None);

                    if (lastSenseCommand != lastSenseMessage)
                    {
                        // New sensation to play
                        lastSenseCommand = lastSenseMessage;
                        lastSenseExecuted = lastSenseTime;

                        // Play the sense -> message received within 5 seconds
                        form.WriteLog("Playing sensation " + senseInfo[0] + "...");

                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // File in use
                form.WriteLog("Failed to access sensations file: " + ex.Message);
            }
            catch (IOException ex)
            {
                // File in use
                form.WriteLog("Failed to access sensations file: " + ex.Message);
            }

        }

        /// <summary>
        /// Reads sensation files
        /// </summary>
        public static void RegisterSensations()
        {
            // Read sensation files
            string configPath = Directory.GetCurrentDirectory() + "\\owo\\";


            // Read all sensations
            foreach (DSSensation dsSensation in Enum.GetValues(typeof(DSSensation)))
            {
                string sensation = Enum.GetName(typeof(DSSensation), dsSensation);
                string fullName = configPath + sensation + ".owo";

                if (!File.Exists(fullName))
                {
                    // File does not exist
                    form.WriteLog("Sensation file for " + sensation + " in " + configPath + " not found!");
                    continue; // Skip
                }

                // Read owo sensation
                string owoSensation = File.ReadAllText(fullName);

                // Add to string list
                BakedSensation sense = BakedSensation.Parse(owoSensation);
                sensations.Add((int)dsSensation, sense);
                OwoSensations.Add(dsSensation, sense);
            }
        }

        /// <summary>
        /// Initiate connection with owoskin
        /// </summary>
        public static Task ConnectOwo(string ip)
        {
            // Connect to owo
            var auth = GameAuth.Create(sensations.Values.ToArray()).WithId("46508921185");
            OWO.Configure(auth);

            form.WriteLog("Attempting to connect to owoskin...");

            if (ip == "")
                return OWO.AutoConnect();

            return OWO.Connect(ip);
        }
    }
}
