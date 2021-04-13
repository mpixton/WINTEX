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
        public GenericRepo<BiologicalSample> BiologicalSamples { get; }
        public GenericRepo<BioSamplesNote> BioSampleNotes { get; }
        public GenericRepo<CarbonDating> CarbonDating { get; }
        public GenericRepo<Fegbdatum> FEGBData { get; }
        public GenericRepo<FegbmummyStorage> FEGBMummyStorage { get; }
        public GenericRepo<FegbstorageLocation> FEGBStorageLocations { get; }
        public GenericRepo<Gisdatum> GISData { get; }
        public GenericRepo<Mummy> Mummies { get; }
        public GenericRepo<MummyNote> MummyNotes { get; }
        public GenericRepo<OsteologicalMummyDatum> MummyBoneData { get; }
        public GenericRepo<PostExhumationDatum> PostExhumationData { get; }
        public GenericRepo<ShaftLocation> ShaftLocations { get; }
        public GenericRepo<TombLocation> TombLocations { get; }
        public GenericRepo<HairColorCodes> HairColorCodes { get; }


        /// <summary>
        /// Saves the changes made to the Repos in this Unit Of Work to the Db.
        /// </summary>
        void Save();
    }
}