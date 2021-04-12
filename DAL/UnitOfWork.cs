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
        private FEGBExcavationContext  _context;
        private readonly ILogger _logger;
        private ILoggerFactory _loggerFactory;

        /// <summary>
        /// Generic Repo for interacting.
        /// </summary>
        private GenericRepo<BiologicalSample> _BiologicalSamples;
        private GenericRepo<BioSamplesNote> _BioSampleNotes;
        private GenericRepo<CarbonDating> _CarbonDating;
        private GenericRepo<Fegbdatum> _FEGBData;
        private GenericRepo<FegbmummyStorage> _FEGBMummyStorage;
        private GenericRepo<FegbstorageLocation> _FEGBStorageLocations;
        private GenericRepo<Gisdatum> _GISData;
        private GenericRepo<Mummy> _Mummies;
        private GenericRepo<MummyNote> _MummyNotes;
        private GenericRepo<OsteologicalMummyDatum> _MummyBoneData;
        private GenericRepo<PostExhumationDatum> _PostExhumationData;
        private GenericRepo<ShaftLocation> _ShaftLocations;
        private GenericRepo<TombLocation> _TombLocations;
        private GenericRepo<HairColorCodes> _HairColorCodes;

        public UnitOfWork(FEGBExcavationContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<UnitOfWork>();
            _logger.LogInformation("Unit of Work with DbContext {contextId} created", _context.ContextId);
        }


        // If the generic repo doesn't exists, create a new one with the DbContext provided by dependency 
        // injection. If already exists, return the current one. Ensures that all Repos are sharing
        // the same DbContext so that changes to mulitple Repos are saved at the same time.
        public GenericRepo<BiologicalSample> BiologicalSamples { get { return _BiologicalSamples ??= new GenericRepo<BiologicalSample>(_context, _loggerFactory.CreateLogger<BiologicalSample>()); } }
        public GenericRepo<BioSamplesNote> BioSampleNotes { get { return _BioSampleNotes ??= new GenericRepo<BioSamplesNote>(_context, _loggerFactory.CreateLogger<BioSamplesNote>()); } }
        public GenericRepo<CarbonDating> CarbonDating { get { return _CarbonDating ??= new GenericRepo<CarbonDating>(_context, _loggerFactory.CreateLogger<CarbonDating>()); } }
        public GenericRepo<Fegbdatum> FEGBData { get { return _FEGBData ??= new GenericRepo<Fegbdatum>(_context, _loggerFactory.CreateLogger<Fegbdatum>()); } }
        public GenericRepo<FegbmummyStorage> FEGBMummyStorage { get { return _FEGBMummyStorage ??= new GenericRepo<FegbmummyStorage>(_context, _loggerFactory.CreateLogger<FegbmummyStorage>()); } }
        public GenericRepo<FegbstorageLocation> FEGBStorageLocations { get { return _FEGBStorageLocations ??= new GenericRepo<FegbstorageLocation>(_context, _loggerFactory.CreateLogger<FegbstorageLocation>()); } }
        public GenericRepo<Gisdatum> GISData { get { return _GISData ??= new GenericRepo<Gisdatum>(_context, _loggerFactory.CreateLogger<Gisdatum>()); } }
        public GenericRepo<Mummy> Mummies { get { return _Mummies ??= new GenericRepo<Mummy>(_context, _loggerFactory.CreateLogger<Mummy>()); } }
        public GenericRepo<MummyNote> MummyNotes { get { return _MummyNotes ??= new GenericRepo<MummyNote>(_context, _loggerFactory.CreateLogger<MummyNote>()); } }
        public GenericRepo<OsteologicalMummyDatum> MummyBoneData { get { return _MummyBoneData ??= new GenericRepo<OsteologicalMummyDatum>(_context, _loggerFactory.CreateLogger<OsteologicalMummyDatum>()); } }
        public GenericRepo<PostExhumationDatum> PostExhumationData { get { return _PostExhumationData ??= new GenericRepo<PostExhumationDatum>(_context, _loggerFactory.CreateLogger<PostExhumationDatum>()); } }
        public GenericRepo<ShaftLocation> ShaftLocations { get { return _ShaftLocations ??= new GenericRepo<ShaftLocation>(_context, _loggerFactory.CreateLogger<ShaftLocation>()); } }
        public GenericRepo<TombLocation> TombLocations { get { return _TombLocations ??= new GenericRepo<TombLocation>(_context, _loggerFactory.CreateLogger<TombLocation>()); } }
        public GenericRepo<HairColorCodes> HairColorCodes { get { return _HairColorCodes ??= new GenericRepo<HairColorCodes>(_context, _loggerFactory.CreateLogger<HairColorCodes>()); } }


        /// <summary>
        /// Saves the chagnes across all Repos in one go.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
