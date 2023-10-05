namespace WebApplication1.Models
{
    public interface IKiralamaRepository : IRepository<Kiralama> 
    {
        void Guncelle(Kiralama kiralama);

        void Kaydet();
    }
}
