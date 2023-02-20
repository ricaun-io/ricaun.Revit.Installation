namespace ricaun.Revit.Installation
{
    /// <summary>
    /// RevitInstallation
    /// </summary>
    public class RevitInstallation
    {
        /// <summary>
        /// InstallLocation
        /// </summary>
        public string InstallLocation { get; }

        /// <summary>
        /// Version
        /// </summary>
        public int Version { get; }

        /// <summary>
        /// RevitInstallation
        /// </summary>
        /// <param name="installLocation"></param>
        /// <param name="version"></param>
        public RevitInstallation(string installLocation, int version)
        {
            InstallLocation = installLocation;
            Version = version;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Revit {0}", Version);
        }
    }
}
