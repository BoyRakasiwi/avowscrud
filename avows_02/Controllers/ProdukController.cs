using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using avows_02.DAL;

namespace avows_02.Models
{
    public class ProdukController : Controller
    {
        ProdukDAL _ProdukDAL=new ProdukDAL();
        // GET: Produk
        public ActionResult Index()
        {
            var produklist=_ProdukDAL.Tampil_all();

            if(produklist.Count==0)
            {
                TempData["InfoMessage"] = "Tidak ada data tersedia di database.";
            }
            return View(produklist);
        }

        // GET: Produk/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produk/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produk/Create
        [HttpPost]
        public ActionResult Create(Produk Produk)
        {
            try
            {
                // TODO: Add insert logic here
                bool _simpan=false;

                if (ModelState.IsValid)
                {
                    _simpan = _ProdukDAL.Simpan(Produk);

                    if (_simpan)
                    {
                        TempData["SuccessMessage"] = "Data berhasil disimpan";
                    }
                    else {
                        TempData["ErrorMessage"] = "Gagal simpan, nama barang sudah ada";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Produk/Edit/5
        public ActionResult Edit(int id)
        {
            var Produk=_ProdukDAL.Get_id(id).FirstOrDefault();
            if (Produk == null)
            {
                TempData["InfoMessage"] = "Kode barang tidak ada";
                return RedirectToAction("Index");
            }
            return View(Produk);
        }

        // POST: Produk/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduk(Produk Produk)
        {
            // TODO: Add update logic here
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdate = _ProdukDAL.Ubah(Produk);

                    if (isUpdate)
                    {
                        TempData["SuccessMessage"] = "Data berhasil diubah.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product is already available/unable to update the product details.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Produk/Delete/5
        public ActionResult Delete(int id)
        {
            var Produk = _ProdukDAL.Get_id(id).FirstOrDefault();
            try
            {
                if (Produk == null)
                {
                    TempData["InfoMessage"] = "Produk tidak ada dengan kode " + id.ToString();
                    return RedirectToAction("Index");
                }

                return View(Produk);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"]=ex.Message;
                return View();
            }
        }

        // POST: Produk/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = _ProdukDAL.HapusProduk(id);

                if (result.Contains("hapus"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else 
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
