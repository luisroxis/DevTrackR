using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTrackR.API.Entities;
using DevTrackR.API.Persistence;

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
      throw new NotImplementedException();
    }

    public Package GetByCode(string code)
    {
      throw new NotImplementedException();
    }

    public void Update(Package package)
    {
      throw new NotImplementedException();
    }
  }
}