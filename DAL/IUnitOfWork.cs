using WINTEX.Models;

namespace WINTEX.DAL
{
    /// <summary>
    /// Defines the members for the Unit Of Work class. 
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Generic Repo for Bowlers.
        /// </summary>
        public GenericRepo<BiologicalSample> BiologicalSamples { get; set; }
        public GenericRepo<BioSamplesNote> BioSampleNotes { get; set; }
        public GenericRepo<CarbonDating> CarbonDating { get; set; }
        public GenericRepo<Fegbdatum> FEGBData { get; set; }
        public GenericRepo<FegbmummyStorage> FEGBMummyStorage { get; set; }
        public GenericRepo<FegbstorageLocation> FEGBStorageLocations { get; set; }
        public GenericRepo<Gisdatum> GISData { get; set; }
        public GenericRepo<Mummy> Mummies { get; set; }
        public GenericRepo<MummyNote> MummyNotes { get; set; }
        public GenericRepo<OsteologicalMummyDatum> MummyBoneData { get; set; }
        public GenericRepo<PostExhumationDatum> PostExhumationData { get; set; }
        public GenericRepo<ShaftLocation> ShaftLocations { get; set; }
        public GenericRepo<TombLocation> TombLocations { get; set; }
        public GenericRepo<HairColorCodes> HairColorCodes { get; set; }


        /// <summary>
        /// Saves the changes made to the Repos in this Unit Of Work to the Db.
        /// </summary>
        void Save();
    }
}