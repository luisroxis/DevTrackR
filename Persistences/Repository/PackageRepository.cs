using DevTrackR.API.Entities;
using DevTrackR.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevTrackR.API.Persistences.Repository
{
  public class PackageRepository : IPackageRepository
  {
    #region Constructor
    private readonly DevTrackRContext _context;
    public PackageRepository(DevTrackRContext context)
    {
      _context = context;
    }
    #endregion

    public void Add(Package package)
    {
      _context.Packages.Add(package);
      _context.SaveChanges();

    }

    public List<Package> GetAll()
    {
      return _context.Packages.ToList();
    }

    public Package GetByCode(string code)
    {
      return _context
        .Packages
        .Include(p => p.Updates)
        .SingleOrDefault(p => p.Code == code);
    }

    public void Update(Package package)
    {
      _context.Entry(package).State = EntityState.Modified;
      _context.SaveChanges();
    }
  }
}