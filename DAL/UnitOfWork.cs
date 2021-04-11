using WINTEX.Models;
using Microsoft.Extensions.Logging;

namespace WINTEX.DAL
{
    /// <summary>
    /// Unit Of Work class. Implements the IUnitOfWork members.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// DbContext to be shared by all Repos in this Unit Of Work. Prevents concurrency issues.
        /// </summary>

        private FEGBExcavationContext _context;

        private readonly ILogger _logger;
        private ILoggerFactory _loggerFactory;

        /// <summary>
        /// Generic Repo for interacting with all Bowlers.
        /// </summary>

        public UnitOfWork(FEGBExcavationContext context, ILoggerFactory loggerFactory)
        {

            _context = context;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<UnitOfWork>();
            _logger.LogInformation("Unit of Work with DbContext created}");
        }


        // If the generic repo doesn't exists, create a new one with the DbContext provided by dependency 
        // injection. If already exists, return the current one. Ensures that all Repos are sharing
        // the same DbContext so that changes to mulitple Repos are saved at the same time.
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
        /// Saves the chagnes across all Repos in one go.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
