using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    

    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository,IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            //List<Kitap> objKitapList=_kitapRepository.GetAll().ToList();
            List<Kitap> objKitapList = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();
            return View(objKitapList);
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()
            });
            ViewBag.KitapTuruList=KitapTuruList;
            if(id == null || id == 0)
            {
                //ekle
                return View();
            }
            else
            {
                //guncelle
                Kitap? kitapVT = _kitapRepository.Get(u => u.Id == id);
                if (kitapVT == null)
                {
                    return NotFound();
                }
                return View(kitapVT);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath=Path.Combine(wwwRootPath,@"img");

                if(file!=null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }

                if(kitap.Id == 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu!";
                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
                }
                _kitapRepository.Kaydet();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }


        /*public ıactionresult Guncelle(int? id)
        {
            if(id==null || id == 0)
            {
                return notfound();
            }
            kitap? kitapvt = _kitaprepository.get(u => u.ıd == id);
            if (kitapvt == null)
            {
                return notfound();
            }
            return view(kitapvt);
        }*/

        /*[HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _kitapRepository.Guncelle(kitap);
                _kitapRepository.Kaydet();
                TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }*/


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVT = _kitapRepository.Get(u => u.Id == id);
            if (kitapVT == null)
            {
                return NotFound();
            }
            return View(kitapVT);
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kitap Başarıyla Silindi!";
            return RedirectToAction("Index", "Kitap");
        }

    }
}
