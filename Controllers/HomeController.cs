using Dapper;
using DataAccess;
using DataAccess.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using LedSiparisModulu.Models;
using LedSiparisModulu.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LedSiparisModulu.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private const string LogoCariEkstra = "SELECT LOGICALREF, CLIENTREF, SOURCEFREF, DATE_ as [IslemTarihi], TRANNO as [BelgeNo], TRCODE, CASE TRCODE WHEN 1 THEN 'Nakit Tahsilat' WHEN 2 THEN 'Nakit Ödeme' WHEN 3 THEN 'Borç Dekontu' WHEN 4 THEN 'Alacak Dekontu' WHEN 5 THEN 'Virman İşlem' WHEN 6 THEN 'Kur Farkı İşlemi' WHEN 14 THEN 'Açılış İşlemi' WHEN 12 THEN 'Özel İşlem' WHEN 20 THEN 'Gelen Havale' WHEN 21 THEN 'Gönderilen Havale' WHEN 31 THEN 'Mal Alım Faturası' WHEN 32 THEN 'Perakende Satış İade Fat.' WHEN 33 THEN 'Toptan Satış İade Fat.' WHEN 34 THEN 'lınan Hizmet Fat.' WHEN 35 THEN 'Alınan Proforma Fat.' WHEN 36 THEN 'Alım İade Fat.'  WHEN 37 THEN 'Perakende Satış Fat.' WHEN 38 THEN 'Toptan Satış Fat.' WHEN 39 THEN 'Verilen Hizmet Faturası' WHEN 40 THEN 'Verilen Proforma Fat.' WHEN 41 THEN 'Verilen Vade Farkı Fat.' WHEN 42 THEN 'Alınan Vade Farkı Fat.' WHEN 43 THEN 'Alınan Fiyat Farkı Fat.' WHEN 44 THEN 'Verilen Fiyat Farkı Fat.' WHEN 56 THEN 'Müsthsil Makbuzu' WHEN 61 THEN 'Çek Girişi' WHEN 62 THEN 'Senet Girişi' WHEN 63 THEN 'Çek Çıkış Cari Hesaba' WHEN 64 THEN 'Senet Çıkış Cari Hesaba' WHEN 70 THEN 'Kredi Kartı Fişi' WHEN 71 THEN 'Kredi Kartı İade Fişi' ELSE 'Tanımsız Evrak' END AS[islemTuru], LINEEXP as [Aciklama], MODULENR, FTIME, AMOUNT as [islemTutari], CYPHCODE, CASE WHEN LGMAIN.SIGN = 0 THEN LGMAIN.AMOUNT ELSE 0 END AS [Borc], CASE WHEN LGMAIN.SIGN = 1 THEN LGMAIN.AMOUNT * -1 ELSE 0 END AS [Alacak], CASE WHEN LGMAIN.SIGN = 0 THEN LGMAIN.AMOUNT ELSE 0 END + CASE WHEN LGMAIN.SIGN = 1 THEN LGMAIN.AMOUNT* -1 ELSE 0 END AS BAKIYE, DOCODE, CANCELLED FROM dbo.LG_050_01_CLFLINE AS LGMAIN WITH (NOLOCK) WHERE CLIENTREF = @CLIENTREF AND (DATE_>=@T1 AND DATE_<=@T2)";
        private const string SIPARISDETAY = "select s.ID, st.KOD  as STOKKOD, st.ISIM as STOKISIM, sd.AMBALAJMIKTAR as SIPARISMIKTAR, (select sab.ISIM from led.stoktanim_ambalajbirim sab where sab.ID = sd.AMBALAJBIRIMID) as AMBALAJBIRIM, s.ACIKLAMA from led.siparis s left join led.depotanim dt on (s.DEPOID=dt.ID), led.siparis_detay sd ,led.wcarikart wc,led.stoktanim st where s.ID =sd.SIPARISID and s.CHID =wc.ID and st.ID = sd.STOKID and sd.SIPARISID=@siparisId";
        private const string FATURADETAYSORGU = "SELECT fd.FATURAID, ch.KOD, ch.ISIM, f.TARIH, f.EVRAKNO, f.IRSALIYENO, st.KOD, st.ISIM, fd.MIKTAR, ISNULL((select top 1 b.ISIM from led.stoktanim_birim b where b.ID= fd.BIRIMID),'') as BIRIM, fd.AMBALAJMIKTAR, ISNULL((select top 1 ab.ISIM from led.stoktanim_ambalajbirim ab where ab.ID= fd.AMBALAJBIRIMID),'') as AMBALAJBIRIM ,fdg.MALTOPLAM, fdg.ISKONTOTOPLAM, fdg.MATRAHTOPLAM, FDG.KDVTOPLAM, FDG.TUTAR from led.chtanim ch, led.fatura f, led.fatura_detay fd, led.fatura_deger fdg, led.stoktanim st WHERE ch.ID=f.CHID and f.ID= fd.FATURAID and fd.STOKID= st.ID and fdg.FATURADETAYID= fd.ID AND fd.FATURAID= @faturaId";
        private const string SIPARISDETAYFIYATLI = "select s.ID, s.TARIH,wc.KOD,wc.ISIM,(select cha.ISIM from led.chtanim_chadres cha where cha.ID = s.SEVKADRESID) as SEVKADRES ,s.EVRAKNO ,st.KOD  as STOKKOD ,st.ISIM as STOKISIM ,CONVERT(NUMERIC(18,3),sd.MIKTAR) as ORTALAMAKG ,CONVERT(NUMERIC(18,0),sd.AMBALAJMIKTAR) AS AMBALAJMIKTAR, ISNULL(CONVERT(NUMERIC(18, 2), sd.FIYAT), 0) as FIYAT ,CONVERT(NUMERIC(18,2),sdd.MALTOPLAM ) AS MALTOPLAM , CONVERT(NUMERIC(18, 2), sdd.ISKONTOTOPLAM) AS ISKONTOTOPLAM , CONVERT(NUMERIC(18, 2), sdd.MATRAHTOPLAM ) AS MATRAHTOPLAM , CONVERT(NUMERIC(18, 2), sdd.KDVTOPLAM    ) AS KDVTOPLAM , CONVERT(NUMERIC(18, 2), sdd.TUTAR) AS TUTAR from led.siparis s, led.siparis_detay sd, led.wcarikart wc, led.stoktanim st, led.siparis_deger sdd where s.ID = @siparisId and s.ID = sd.SIPARISID and s.ID = sdd.SIPARISID and sd.SIPARISID= sdd.SIPARISID and s.CHID = wc.ID and st.ID = sd.STOKID and sdd.SIPARISDETAYID= sd.ID and ('0'='0' and (s.TARIH>= @baslangicTarihi) and(s.TARIH<=@bitisTarihi)) and(('0' = '1') or(('0' = '0') and(s.CHID IN (@userId)))) and (('0' = '0') or (('0' = '0') and  (s.SEVKADRESID IN (@sevkAdresId)))) and((''='') or(s.OZELKOD= '')) order by s.TARIH,s.SAAT";
        private const string SİPARİSKARSİLAMA = "CREATE TABLE #siparis (STOKKOD nvarchar(250) null, STOKISIM nvarchar(250) null, OZELKOD2 nvarchar(250) null, OZELKOD3 nvarchar(250) null, OZELKOD4 nvarchar(250) null, OZELKOD5 nvarchar(250) null, PARCAGRUBU nvarchar(250) null, PARCAGRUBU2 nvarchar(250) null, ORTALAMAKOLIKG numeric(18,3) DEFAULT(0), HAMSIPARISKG numeric(18,3) DEFAULT(0), HAMSIPARISKOLI numeric(18,0) DEFAULT(0), OPTIMIZESIPARISKG numeric(18,3) DEFAULT(0), OPTIMIZESIPARISKOLI numeric(18,0) DEFAULT(0), OPTIMIZEKARSILAMAORAN numeric(18,2) DEFAULT(0), SEVKKG numeric(18,3) DEFAULT(0), SEVKKOLI numeric(18,0) DEFAULT(0), SEVKKARSILAMAORAN numeric(18,2) DEFAULT(0), HAMDANSEVKKARSILAMAORAN numeric(18,2) DEFAULT(0)) INSERT INTO #siparis (STOKKOD, STOKISIM, OZELKOD2, OZELKOD3, OZELKOD4, OZELKOD5, PARCAGRUBU, PARCAGRUBU2, ORTALAMAKOLIKG) SELECT KOD, ISIM, OZELKOD2, OZELKOD3, OZELKOD4, OZELKOD5, PARCAGRUBU, PARCAGRUBU2, ORTALAMAKOLIKG FROM led.stoktanim WHERE LISTESIRANO IS NOT NULL UPDATE #siparis SET HAMSIPARISKG = S1.SIPARISMIKTAR, HAMSIPARISKOLI = S1.AMBALAJMIKTAR from ( select s.EVRAKNO, wc.KOD, wc.ISIM, (select cha.ISIM from led.chtanim_chadres cha where cha.ID = s.SEVKADRESID) as SEVKADRES, st.KOD as STOKKOD, st.ISIM as STOKISIM, (select ISIM  from led.kategori where ID = st.KATEGORI2ID) as STOKKATEGORI2, st.PARCAGRUBU2 as PARCAGRUBU, sd.MIKTAR as SIPARISMIKTAR, sd.AMBALAJMIKTAR as AMBALAJMIKTAR from led.siparis s left join led.depotanim dt on (s.DEPOID= dt.ID),led.siparis_detay sd, led.wcarikart wc, led.stoktanim st where s.ID =sd.SIPARISID and s.CHID = wc.ID and st.ID = sd.STOKID and (('0'='0' and (s.TARIH= CAST(@siparisTarihi as datetime)))) and (s.EVRAKNO= 'Web Sipariş') and (('0' = '1') or (('0' = '0') and (s.CHID IN (@userId)))) and (('0' = '1') or (('0' = '0') and (s.SEVKADRESID IN (@sevkAdresDDL))))) as S1 WHERE S1.STOKKOD=#siparis.STOKKOD UPDATE #siparis SET OPTIMIZESIPARISKG= S1.SIPARISMIKTAR, OPTIMIZESIPARISKOLI = S1.AMBALAJMIKTAR from ( select s.EVRAKNO, wc.KOD, wc.ISIM, (select cha.ISIM from led.chtanim_chadres cha where cha.ID = s.SEVKADRESID) as SEVKADRES, st.KOD as STOKKOD, st.ISIM as STOKISIM, (select ISIM from led.kategori where ID = st.KATEGORI2ID) as STOKKATEGORI2, st.PARCAGRUBU2 as PARCAGRUBU, sd.MIKTAR as SIPARISMIKTAR, sd.AMBALAJMIKTAR as AMBALAJMIKTAR from led.siparis s left join led.depotanim dt on (s.DEPOID= dt.ID), led.siparis_detay sd, led.wcarikart wc, led.stoktanim st where s.ID =sd.SIPARISID and s.CHID = wc.ID and st.ID = sd.STOKID and (('0'='0' and (s.TARIH= CAST(@siparisTarihi as datetime)))) and (s.EVRAKNO= 'Optimize') and (('0' = '1') or (('0' = '0') and (s.CHID IN (@userId)))) and (('0' = '1') or (('0' = '0') and (s.SEVKADRESID IN (@sevkAdresDDL))))) as S1 WHERE S1.STOKKOD=#siparis.STOKKOD UPDATE #siparis SET OPTIMIZEKARSILAMAORAN = ISNULL (NULLIF (OPTIMIZESIPARISKOLI, 0) / NULLIF (HAMSIPARISKOLI, 0)*100,0) UPDATE #siparis SET SEVKKG = S1.MIKTAR, SEVKKOLI = S1.AMBALAJMIKTAR from ( select s.EVRAKSERI + s.EVRAKSIRA as EVRAKNO, ch.KOD as CHKOD, ch.ISIM as CHISIM, (select cha.ISIM from led.chtanim_chadres cha where cha.ID = s.SEVKADRESID) as SEVKADRES, st.KOD as STOKKOD, st.ISIM as STOKISIM, (select top 1 ISIM from led.kategori k where k.ID = st.KATEGORI1ID) as STKATEGORI01, st.PARCAGRUBU2, ( CASE WHEN s.GC='C' THEN s.MIKTAR ELSE s.MIKTAR* -1 END) as MIKTAR, ( CASE WHEN s.GC='C' THEN s.CIKANAMBALAJMIKTAR ELSE s.GIRENAMBALAJMIKTAR * -1 END) as AMBALAJMIKTAR from led.wstokhareket s left join led.stoktanim st on (st.ID= s.STOKID) left join led.wcarikart ch on (ch.ID= s.CHID) left join led.chtanim_chadres cha on (cha.ID= s.SEVKADRESID) where 1=1 and s.TARIH= CAST(@sevkTarihi  as datetime)and(('0' ='1') or(('0' = '0') and(ch.ID IN (@userId))))and(('0' ='1') or(('0' = '0') and (s.SEVKADRESID IN (@sevkAdresDDL)))) and s.TIP IN ('F2','I2')) as S1 WHERE S1.STOKKOD=#siparis.STOKKOD UPDATE #siparis SET SEVKKARSILAMAORAN = ISNULL (NULLIF (SEVKKOLI, 0) / NULLIF (OPTIMIZESIPARISKOLI, 0) * 100,0) UPDATE #siparis SET HAMDANSEVKKARSILAMAORAN= ISNULL(NULLIF(SEVKKOLI,0)/NULLIF(HAMSIPARISKOLI,0)*100,0) SELECT * FROM #siparis WHERE HAMSIPARISKOLI>0 OR OPTIMIZESIPARISKOLI>0 OR SEVKKOLI>0 drop table #siparis";
        private const string SİPARİSKARSİLAMASEVKSİZ = "CREATE TABLE #siparis (STOKKOD nvarchar(250) null, STOKISIM nvarchar(250) null, OZELKOD2 nvarchar(250) null, OZELKOD3 nvarchar(250) null, OZELKOD4 nvarchar(250) null, OZELKOD5 nvarchar(250) null, PARCAGRUBU nvarchar(250) null, PARCAGRUBU2 nvarchar(250) null, ORTALAMAKOLIKG numeric(18,3) DEFAULT(0), HAMSIPARISKG numeric(18,3) DEFAULT(0), HAMSIPARISKOLI numeric(18,0) DEFAULT(0), OPTIMIZESIPARISKG numeric(18,3) DEFAULT(0), OPTIMIZESIPARISKOLI numeric(18,0) DEFAULT(0), OPTIMIZEKARSILAMAORAN numeric(18,2) DEFAULT(0), SEVKKG numeric(18,3) DEFAULT(0), SEVKKOLI numeric(18,0) DEFAULT(0), SEVKKARSILAMAORAN numeric(18,2) DEFAULT(0), HAMDANSEVKKARSILAMAORAN numeric(18,2) DEFAULT(0)) INSERT INTO #siparis (STOKKOD, STOKISIM, OZELKOD2, OZELKOD3, OZELKOD4, OZELKOD5, PARCAGRUBU, PARCAGRUBU2, ORTALAMAKOLIKG) SELECT KOD, ISIM, OZELKOD2, OZELKOD3, OZELKOD4, OZELKOD5, PARCAGRUBU, PARCAGRUBU2, ORTALAMAKOLIKG FROM led.stoktanim WHERE LISTESIRANO IS NOT NULL UPDATE #siparis SET HAMSIPARISKG = S1.SIPARISMIKTAR, HAMSIPARISKOLI = S1.AMBALAJMIKTAR from ( select s.EVRAKNO, wc.KOD, wc.ISIM, (select cha.ISIM from led.chtanim_chadres cha where cha.ID = s.SEVKADRESID) as SEVKADRES, st.KOD as STOKKOD, st.ISIM as STOKISIM, (select ISIM  from led.kategori where ID = st.KATEGORI2ID) as STOKKATEGORI2, st.PARCAGRUBU2 as PARCAGRUBU, sd.MIKTAR as SIPARISMIKTAR, sd.AMBALAJMIKTAR as AMBALAJMIKTAR from led.siparis s left join led.depotanim dt on (s.DEPOID= dt.ID),led.siparis_detay sd, led.wcarikart wc, led.stoktanim st where s.ID =sd.SIPARISID and s.CHID = wc.ID and st.ID = sd.STOKID and (('0'='0' and (s.TARIH= CAST(@siparisTarihi as datetime)))) and (s.EVRAKNO= 'Web Sipariş') and (('0' = '1') or (('0' = '0') and (s.CHID IN (@userId))))) as S1 WHERE S1.STOKKOD=#siparis.STOKKOD UPDATE #siparis SET OPTIMIZESIPARISKG= S1.SIPARISMIKTAR, OPTIMIZESIPARISKOLI = S1.AMBALAJMIKTAR from ( select s.EVRAKNO, wc.KOD, wc.ISIM, (select cha.ISIM from led.chtanim_chadres cha where cha.ID = s.SEVKADRESID) as SEVKADRES, st.KOD as STOKKOD, st.ISIM as STOKISIM, (select ISIM from led.kategori where ID = st.KATEGORI2ID) as STOKKATEGORI2, st.PARCAGRUBU2 as PARCAGRUBU, sd.MIKTAR as SIPARISMIKTAR, sd.AMBALAJMIKTAR as AMBALAJMIKTAR from led.siparis s left join led.depotanim dt on (s.DEPOID= dt.ID), led.siparis_detay sd, led.wcarikart wc, led.stoktanim st where s.ID =sd.SIPARISID and s.CHID = wc.ID and st.ID = sd.STOKID and (('0'='0' and (s.TARIH= CAST(@siparisTarihi as datetime)))) and (s.EVRAKNO= 'Optimize') and (('0' = '1') or (('0' = '0') and (s.CHID IN (@userId))))) as S1 WHERE S1.STOKKOD=#siparis.STOKKOD UPDATE #siparis SET OPTIMIZEKARSILAMAORAN = ISNULL (NULLIF (OPTIMIZESIPARISKOLI, 0) / NULLIF (HAMSIPARISKOLI, 0)*100,0) UPDATE #siparis SET SEVKKG = S1.MIKTAR, SEVKKOLI = S1.AMBALAJMIKTAR from ( select s.EVRAKSERI + s.EVRAKSIRA as EVRAKNO, ch.KOD as CHKOD, ch.ISIM as CHISIM, (select cha.ISIM from led.chtanim_chadres cha where cha.ID = s.SEVKADRESID) as SEVKADRES, st.KOD as STOKKOD, st.ISIM as STOKISIM, (select top 1 ISIM from led.kategori k where k.ID = st.KATEGORI1ID) as STKATEGORI01, st.PARCAGRUBU2, ( CASE WHEN s.GC='C' THEN s.MIKTAR ELSE s.MIKTAR* -1 END) as MIKTAR, ( CASE WHEN s.GC='C' THEN s.CIKANAMBALAJMIKTAR ELSE s.GIRENAMBALAJMIKTAR * -1 END) as AMBALAJMIKTAR from led.wstokhareket s left join led.stoktanim st on (st.ID= s.STOKID) left join led.wcarikart ch on (ch.ID= s.CHID) left join led.chtanim_chadres cha on (cha.ID= s.SEVKADRESID) where 1=1 and s.TARIH= CAST(@sevkTarihi  as datetime)and(('0' ='1') or(('0' = '0') and(ch.ID IN (@userId))))) as S1 WHERE S1.STOKKOD=#siparis.STOKKOD UPDATE #siparis SET SEVKKARSILAMAORAN = ISNULL (NULLIF (SEVKKOLI, 0) / NULLIF (OPTIMIZESIPARISKOLI, 0) * 100,0) UPDATE #siparis SET HAMDANSEVKKARSILAMAORAN= ISNULL(NULLIF(SEVKKOLI,0)/NULLIF(HAMSIPARISKOLI,0)*100,0) SELECT * FROM #siparis WHERE HAMSIPARISKOLI>0 OR OPTIMIZESIPARISKOLI>0 OR SEVKKOLI>0 drop table #siparis";


        private readonly LAF_BUPILICContext db;
        private readonly LogoDbContext logodb;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(LAF_BUPILICContext context,
            LogoDbContext logocontext,
            IConfiguration configuration,
            ILogger<HomeController> logger)
        {
            db = context;
            logodb = logocontext;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("")]
        [HttpGet("anasayfa", Name = "Anasayfa")]
        public IActionResult Index()
        {
            long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);

            var sevkAdresList = db.ChAdres.AsNoTracking().Where(x => x.ChId == userID).ToList();
            ViewData["sevkAdresList"] = new SelectList(sevkAdresList, "Id", "Isim");

            var doluMu = db.Websepet.AsNoTracking().Where(x => x.Chid == userID).Count();
            if (doluMu > 0)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Sepetiniz de onaylanmayı bekleyen ürünler var", Infrastructures.ToastType.warning);
            }

            var anketKatilim = db.Anket.AsNoTracking().Where(x => x.ChId == userID).Count();
            if (anketKatilim < 1)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Sizin için 1 adet anketimiz mevcut !", Infrastructures.ToastType.info);
            }

            return View();
        }

        [HttpGet("anket", Name = "Anket")]
        public IActionResult Anket()
        {
            long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);
            var anketKatilim = db.Anket.AsNoTracking().Where(x => x.ChId == userID).Count();
            if (anketKatilim >= 1)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Şuan da mevcut bir anket bulunmamaktadır !", Infrastructures.ToastType.info);
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpPost("anket-ekle", Name = "AnketEklePost")]
        public ActionResult AnketEkle(AnketEkleViewModel model)
        {
            long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);
            string unvan = User.FindFirst("Chunvan").Value;

            Anket eklenecekAnket = new Anket();
            eklenecekAnket.ChId = userID;
            eklenecekAnket.Unvan = unvan;
            eklenecekAnket.Tarih = DateTime.Now;
            eklenecekAnket.Soru1 = model.Soru1;
            eklenecekAnket.Soru2 = model.Soru2;
            eklenecekAnket.Soru3 = model.Soru3;
            eklenecekAnket.Soru4 = model.Soru4;
            eklenecekAnket.Soru5 = model.Soru5;
            eklenecekAnket.Soru6 = model.Soru6;
            eklenecekAnket.Metin = model.Metin;

            db.Anket.Add(eklenecekAnket);
            db.SaveChanges();
            return Json(new { redirectUrl = Url.RouteUrl("Anasayfa")});
        }

        [HttpGet("anket-sonuc", Name = "AnketSonuc")]
        public IActionResult AnketSonuc()
        {
            return View();
        }

        [HttpGet("yeni-siparis", Name = "YeniSiparis")]
        public IActionResult Siparis()
        {
            long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);

            var sevkAdresList = db.ChAdres.AsNoTracking().Where(x => x.ChId == userID && x.Aktif == true).ToList();
            ViewData["sevkAdresList"] = new SelectList(sevkAdresList, "Id", "Isim");         

            List<SepetVM> sepet = new List<SepetVM>();
            var sepetDB = db.Websepet.AsNoTracking()
                .Where(x => x.Chid == userID)
                .ToList();

            foreach (var sepetItem in sepetDB)
            {
                var stokItem = db.Stoktanim.AsNoTracking().FirstOrDefault(x => x.Id == sepetItem.Stokid);
                sepet.Add(new SepetVM()
                {
                    StokId = sepetItem.Stokid,
                    Birim = sepetItem.Birim,
                    Miktar = sepetItem.Miktar,
                    StokIsim = stokItem.Stokisim,
                    StokKod = stokItem.Stokkod
                });
            }
            return View(sepet);
        }

        [HttpPost]
        public IActionResult SepeteEkle(SepeteEkleVM sepet)
        {
            //Websepette bu stokid'li urun varsa miktarini guncelle yoksa ekle

            if (float.IsNaN(sepet.Miktar) || sepet.Miktar <= 0)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Miktar boş geçilemez", Infrastructures.ToastType.warning);
                return Json("Miktar boş geçilemez");
            }

            var userId = long.Parse(HttpContext.User.FindFirst("Id").Value);
            var sepetDB = db.Websepet.FirstOrDefault(x => x.Stokid == sepet.StokId && x.Chid == userId);

            if (sepetDB == null)
            {
                db.Websepet.Add(new Websepet()
                {
                    Chid = long.Parse(HttpContext.User.FindFirst("Id").Value),
                    Stokid = sepet.StokId,
                    Birim = sepet.Birim,
                    Miktar = sepet.Miktar,
                });
            }
            else
            {
                sepetDB.Miktar += sepet.Miktar;
            }

            db.SaveChanges();
            return Json("");
        }

        [HttpPost]
        public IActionResult SepettenCikar(long sepettekiStokId)
        {
            long userId = long.Parse(HttpContext.User.FindFirst("Id").Value);
            db.Websepet.Remove(db.Websepet.FirstOrDefault(x => x.Stokid == sepettekiStokId && x.Chid == userId));
            db.SaveChanges();
            return Json("");
        }

        [HttpPost]
        public IActionResult SiparisIptal(long siparisId)
        {
            using (SqlConnection lafBuPilic = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
            {
                lafBuPilic.Open();
                var transaction = lafBuPilic.BeginTransaction();
                try
                {
                    if (lafBuPilic.ExecuteScalar<int>("SELECT Count(Id) FROM [led].[siparis] (NOLOCK) WHERE ID = @SiparisId", new { SiparisId = siparisId }, transaction) > 0)
                    {
                        var siparisDetay = lafBuPilic.QueryFirst<SiparisDetay>("SELECT * FROM [led].[siparis] (NOLOCK) WHERE ID = @SiparisId", new { SiparisId = siparisId }, transaction);

                        if (siparisDetay.Aciklama == "Web Sipariş")
                        {
                            lafBuPilic.Execute("DELETE FROM [led].siparis WHERE ID = @SiparisId", new { SiparisId = siparisId }, transaction);
                            lafBuPilic.Execute("DELETE FROM [led].siparis_deger WHERE SIPARISID = @SiparisId", new { SiparisId = siparisId }, transaction);
                            lafBuPilic.Execute("DELETE FROM [led].siparis_detay WHERE SIPARISID = @SiparisId", new { SiparisId = siparisId }, transaction);
                        }
                        else
                        {
                            return Json("Optimize siparişler silinemez");
                        }

                        //Siparis İptal Log
                        long userId = long.Parse(HttpContext.User.FindFirst("Id").Value);
                        lafBuPilic.Execute("INSERT INTO iptalsiparisLog (SIPARISID, CHID, IPTALTARIHI) VALUES (@SiparisId, @userId, @tarih)", new { SiparisId = siparisId, userId, tarih = DateTime.Now }, transaction);


                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, ex.Message);
                    return Json("Beklenmeyen bir hata oluştu.");
                }
            }
            return Json("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SiparisOnayla(SiparisOnaylaVM siparis)
        {
            if (siparis.SiparisTarihi == DateTime.MinValue)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", " Sipariş Tarihi Boş Geçilemez", Infrastructures.ToastType.error);
                return RedirectToRoute("YeniSiparis");
            }
            else if (siparis.TeminTarih == DateTime.MinValue)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Temin tarihi Boş Geçilemez", Infrastructures.ToastType.error);
                return RedirectToRoute("YeniSiparis");
            }
            else if (siparis.TeslimTarih == DateTime.MinValue)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Teslim tarihi Boş Geçilemez", Infrastructures.ToastType.error);
                return RedirectToRoute("YeniSiparis");
            }

            try
            {
                long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);
                var sevkID = db.ChAdres.FirstOrDefault(x => x.ChId == userID && x.Id == siparis.SevkAdresId && x.Aktif == true);

                var sepet = db.Websepet.Where(x => x.Chid == userID).ToList();
                if (sepet.Count == 0)
                {
                    AddToastMessage("Bupiliç Entegre A.Ş.", "Sepet Boş Geçilemez", Infrastructures.ToastType.error);
                    return RedirectToRoute("YeniSiparis");
                }

                var sevkAdresVarMı = db.ChAdres.FirstOrDefault(x => x.ChId == userID);
          
                if (sevkAdresVarMı != null && sevkAdresVarMı.Aktif == true && siparis.SevkAdresId == 0)
                {
                    AddToastMessage("Bupiliç Entegre A.Ş.", "Sevk Adresi Boş Geçilemez", Infrastructures.ToastType.error);
                    return RedirectToRoute("YeniSiparis");
                }

                using (SqlConnection LafBuPilic = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlWriter.WriteStartElement("SIPARIS");

                    xmlWriter.WriteStartElement("KALEMLER");
                    foreach (var siparisItem in sepet)
                    {
                        var stokItem = db.Stoktanim.AsNoTracking().FirstOrDefault(x => x.Id == siparisItem.Stokid);
                        xmlWriter.WriteStartElement("KALEM");
                        xmlWriter.WriteElementString("STOKID", stokItem.Id.ToString());
                        xmlWriter.WriteElementString("AMBALAJBIRIM", stokItem.Ambalajbirim);
                        xmlWriter.WriteElementString("AMBALAJMIKTAR", siparisItem.Miktar.ToString());
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("BASLIK");

                    xmlWriter.WriteElementString("CHID", userID.ToString());
                    if (sevkID != null)
                    {
                        xmlWriter.WriteElementString("SEVKADRESID", sevkID.Id.ToString());
                    }
                    xmlWriter.WriteElementString("TARIH", siparis.SiparisTarihi.ToString("MM.dd.yyyy"));
                    xmlWriter.WriteElementString("TESLIMTARIH", siparis.TeslimTarih.ToString("MM.dd.yyyy"));
                    xmlWriter.WriteElementString("SIPARISNOTU", siparis.SiparisNotu);

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.Close();

                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@xml", stringWriter.ToString(), DbType.Xml, ParameterDirection.Input);
                    var siparisOlusmaSonucu = LafBuPilic.QueryFirst<SiparisOlusturXmlModel>("sp_webapp_siparis_kaydet", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                    if (siparisOlusmaSonucu.SONUC >= 0)
                    {
                        AddToastMessage("Bupiliç Entegre A.Ş.", siparisOlusmaSonucu.MESAJ, Infrastructures.ToastType.success);
                        foreach (var cartItem in sepet)
                        {
                            db.Websepet.Remove(cartItem);
                        }
                        db.SaveChanges();
                        return RedirectToRoute("YeniSiparis");
                    }
                    else if (siparisOlusmaSonucu.SONUC == -1)
                    {
                        AddToastMessage("Bupiliç Entegre A.Ş.", siparisOlusmaSonucu.MESAJ, Infrastructures.ToastType.warning);
                        return RedirectToRoute("YeniSiparis");
                    }
                    else if (siparisOlusmaSonucu.SONUC >= -2)
                    {
                        AddToastMessage("Bupiliç Entegre A.Ş.", siparisOlusmaSonucu.MESAJ, Infrastructures.ToastType.warning);
                        return RedirectToRoute("YeniSiparis");
                    }
                }
                return RedirectToAction("Siparis");
            }
            catch (Exception ex)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Siparişiniz Oluşturulamadı,Tekrar Deneyiniz", Infrastructures.ToastType.error);
                return RedirectToAction("Siparis");
            }
        }

        [HttpGet("siparis-karsilama", Name = "SiparisKarsilama")]
        public IActionResult SiparisKarsilama()
        {
            long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);

            var sevkAdresList = db.ChAdres.AsNoTracking().Where(x => x.ChId == userID).ToList();
            ViewData["sevkAdresList"] = new SelectList(sevkAdresList, "Id", "Isim");
            return View();
        }


        //Siparis Karsilama
        public async Task<IActionResult> SiparisKarsilama(IDataTablesRequest request, 
            [FromQuery] DateTime? siparisTarihi = null,
            [FromQuery] DateTime? sevkTarihi = null,
            [FromQuery] long? sevkAdresDDL = null        
            )
        {

            if (request.AdditionalParameters != null)
            {
                if (request.AdditionalParameters.TryGetValue("siparisTarihi", out object siparisTarihiString))
                {
                    siparisTarihi = DateTime.Parse(siparisTarihiString.ToString());
                }
                if (request.AdditionalParameters.TryGetValue("sevkTarihi", out object sevkTarihiString))
                {
                    sevkTarihi = DateTime.Parse(sevkTarihiString.ToString());
                }
                if (request.AdditionalParameters.TryGetValue("sevkAdresDDL", out object SevkAdresDDLString))
                {
                    sevkAdresDDL = long.Parse(SevkAdresDDLString.ToString());
                }
                else
                {
                    using (var connection = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
                    {
                        long userId = long.Parse(HttpContext.User.FindFirst("Id").Value);
                        var result = await connection.QueryAsync<SiparisKarsilamaVM>(SİPARİSKARSİLAMASEVKSİZ, new { userId, siparisTarihi,sevkTarihi });

                        var response = DataTablesResponse.Create(request, result.Count(), result.ToList().Count(), result);
                        return new DataTablesJsonResult(response);
                    }
                }
            }

            using (var connection = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
            {
                long userId = long.Parse(HttpContext.User.FindFirst("Id").Value);
                var result = await connection.QueryAsync<SiparisKarsilamaVM>(SİPARİSKARSİLAMA, new { userId,siparisTarihi,sevkTarihi,sevkAdresDDL });
                           
                var response = DataTablesResponse.Create(request, result.Count(), result.ToList().Count(),result);
                return new DataTablesJsonResult(response);
            }
        }

        //Anket Sonuçları
        public IActionResult GetAnketSonucu(IDataTablesRequest request)
        {
            var data = db.Anket.AsNoTracking();
            IQueryable<Anket> filteredData = data;
            if (request.Search.Value != null)
            {
                filteredData = filteredData.Where(_item => _item.Metin.Contains(request.Search.Value));
            }

            var dataPage = filteredData.OrderByDescending(o => o.Metin).Skip(request.Start).Take(request.Length);
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
            return new DataTablesJsonResult(response);
        }

        //Ürün Listesi tablosunu doldur
        public IActionResult GetUrunListesi(IDataTablesRequest request)
        {

            var data = db.Stoktanim.AsNoTracking().Where(x => x.Siparislistevar == true);
            IQueryable<Stoktanim> filteredData = data;
            if (request.Search.Value != null)
            {
                filteredData = filteredData.Where(_item => _item.Stokisim.Contains(request.Search.Value) || _item.Stokkod.Contains(request.Search.Value));
            }

            var dataPage = filteredData.OrderByDescending(o => o.Stokisim);//.Skip(request.Start).Take(request.Length);
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
            return new DataTablesJsonResult(response);
        }

        //Cari ekstre tablosunu doldur
        public IActionResult GetCariEkstre(IDataTablesRequest request)
        {
            DateTime? baslangicTarihi = null, bitisTarihi = null;

            if (request.AdditionalParameters != null)
            {
                if (request.AdditionalParameters.TryGetValue("baslangicTarihi", out object baslangicTarihiString))
                {
                    baslangicTarihi = DateTime.Parse(baslangicTarihiString.ToString());
                }

                if (request.AdditionalParameters.TryGetValue("bitisTarihi", out object bitisTarihiString))
                {
                    bitisTarihi = DateTime.Parse(bitisTarihiString.ToString());
                }
            }

            long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);
            string kod = HttpContext.User.FindFirst("Chkod").Value;
            var carikod = logodb.LG_050_CLCARD.AsNoTracking().FirstOrDefault(x => x.Code == kod);

            using (SqlConnection logoConnection = new SqlConnection(_configuration.GetSection("ConnectionString:LogoConnection").Value))
            {
                var cari = logoConnection.Query<CariEkstraModel>(LogoCariEkstra, new { CLIENTREF = carikod.LogicalRef, T1 = baslangicTarihi, T2 = bitisTarihi });
                if (request.Search.Value != null)
                {
                    cari = cari.Where(_item => _item.IslemTuru.Contains(request.Search.Value));
                }
                var dataPage = cari.OrderByDescending(o => o.IslemTuru).Skip(request.Start).Take(request.Length).ToList();
                var response = DataTablesResponse.Create(request, cari.Count(), cari.Count(), dataPage);
                return new DataTablesJsonResult(response);
            }
        }

        //Siparislerim tablosunu doldur
        public IActionResult GetSiparisler(IDataTablesRequest request)
        {
            DateTime? baslangicTarihi = null, bitisTarihi = null;
            string siparisDurum = null;

            if (request.AdditionalParameters != null)
            {
                if (request.AdditionalParameters.TryGetValue("baslangicTarihi", out object baslangicTarihiString))
                {
                    baslangicTarihi = DateTime.Parse(baslangicTarihiString.ToString());
                }

                if (request.AdditionalParameters.TryGetValue("bitisTarihi", out object bitisTarihiString))
                {
                    bitisTarihi = DateTime.Parse(bitisTarihiString.ToString());
                }

                if (request.AdditionalParameters.TryGetValue("siparisDurum", out object siparisDurumString))
                {
                    siparisDurum = siparisDurumString.ToString();
                }
            }

            string chKod = (HttpContext.User.FindFirst("ChKod").Value);
            var data = db.SiparisDetay.AsNoTracking()
                .Where(x => x.Kod == chKod && x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi);
            IQueryable<SiparisDetay> filteredData = data;
            if (request.Search.Value != null)
            {
                filteredData = data.Where(_item => _item.Aciklama.Contains(request.Search.Value));
            }
            var dataPage = filteredData.OrderByDescending(o => o.Aciklama).Skip(request.Start).Take(request.Length);
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
            return new DataTablesJsonResult(response);
        }

        //Siparislerimin detay tablosunu doldur
        [HttpGet("siparis-detay-fiyatli", Name = "SiparisDetayFiyatli")]
        public async Task<IActionResult> SiparisDetayFiyatli(
            [FromQuery] long siparisId,
            [FromQuery] DateTime baslangicTarihi,
            [FromQuery] DateTime bitisTarihi,
            [FromQuery] long? sevkAdresId)
        {
            using (var connection = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
            {
                long userId = long.Parse(HttpContext.User.FindFirst("Id").Value);
                var result = await connection.QueryAsync<SiparisDetayFiyatliDataGrid>(SIPARISDETAYFIYATLI, new { userId, siparisId, baslangicTarihi, bitisTarihi, sevkAdresId });
                return PartialView("_SiparisDetay", Tuple.Create(siparisId, result));
            }
        }

        [HttpPost]
        public IActionResult GetFatura(IDataTablesRequest request)
        {
            DateTime? baslangicTarihi = null, bitisTarihi = null;

            if (request.AdditionalParameters != null)
            {
                if (request.AdditionalParameters.TryGetValue("baslangicTarihi", out object baslangicTarihiString))
                {
                    baslangicTarihi = DateTime.Parse(baslangicTarihiString.ToString());
                }

                if (request.AdditionalParameters.TryGetValue("bitisTarihi", out object bitisTarihiString))
                {
                    bitisTarihi = DateTime.Parse(bitisTarihiString.ToString());
                }
            }

            long userID = long.Parse(HttpContext.User.FindFirst("Id").Value);

            var data = db.FaturaListesi.AsNoTracking()
                .Where(x => x.UserId == userID && x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi && x.TipStr == "Satış Faturası");
            IQueryable<FaturaListesi> filteredData = data;
            if (request.Search.Value != null)
            {
                filteredData = data.Where(_item => _item.IrsaliyeNo.Contains(request.Search.Value) || _item.BelgeNo.Contains(request.Search.Value));
            }
            var dataPage = filteredData.OrderByDescending(o => o.IrsaliyeNo).Skip(request.Start).Take(request.Length);
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
            return new DataTablesJsonResult(response);
        }

        [HttpGet(Name = "FaturaDetay")]
        public async Task<IActionResult> FaturaDetay([FromQuery] long faturaId)
        {
            using (var connection = new SqlConnection(_configuration.GetSection("ConnectionString:DefaultConnection").Value))
            {
                var result = await connection.QueryAsync<FaturaDetayDataGrid>(FATURADETAYSORGU, new { faturaId });
                return PartialView("_FaturaDetay", result);
            }
        }

        [HttpGet("fatura-detay", Name = "FaturaDetay")]
        public IActionResult FaturaDetay()
        {
            return View();
        }
        [HttpGet("cari-ekstre", Name = "CariEkstre")]
        public IActionResult CariEkstre()
        {
            return View();
        }

        [HttpGet("siparis-detay", Name = "SiparisDetay")]
        public IActionResult SiparisDetay()
        {
            return View();
        }      

        [HttpGet("kampanya-liste", Name = "KampanyaListe")]
        public IActionResult KampanyaListe()
        {
            var kampanyaList = db.Kampanyalar.AsNoTracking().Where(x => x.Durum == true && x.BitisTarihi >= DateTime.Now).ToList();
            return View(kampanyaList);
        }

        [HttpGet("kampanya-olustur", Name = "KampanyaOlustur")]
        public IActionResult KampanyaOlustur()
        {
            return View();
        }

        [HttpPost("kampanya-olustur", Name = "KampanyaOlusturPost")]
        public ActionResult KampanyaOlustur(KampanyaOlusturViewModel model)
        {
            if (model.BaslangicTarihi == DateTime.MinValue)
            {
                AddToastMessage("Hata", "Başlangıç tarihi boş geçilemez");
                return View();
            }

            Kampanyalar olusacakKampanya = new Kampanyalar();
            olusacakKampanya.Baslik = model.Baslik;
            olusacakKampanya.BaslangicTarihi = model.BaslangicTarihi;
            olusacakKampanya.BitisTarihi = model.BitisTarihi;
            olusacakKampanya.Metin = model.Metin;
            olusacakKampanya.KayitTarihi = DateTime.Now;
            olusacakKampanya.Durum = true;

            db.Kampanyalar.Add(olusacakKampanya);
            db.SaveChanges();
            return RedirectToRoute("Anasayfa");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
