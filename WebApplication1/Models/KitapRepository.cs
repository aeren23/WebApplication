using System.Linq.Expressions;
using WebApplication1.Utility;

namespace WebApplication1.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public KitapRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Kitap kitap)
        {
            _uygulamaDbContext.Update(kitap);      
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
