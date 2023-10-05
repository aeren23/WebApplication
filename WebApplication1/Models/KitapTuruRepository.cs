using System.Linq.Expressions;
using WebApplication1.Utility;

namespace WebApplication1.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>,  IKitapTuruRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public KitapTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(KitapTuru kitapTuru)
        {
            _uygulamaDbContext.Update(kitapTuru);      
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
