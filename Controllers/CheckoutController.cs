﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Layout.Db;
using Layout.Models;
using Microsoft.AspNetCore.Mvc;

namespace Layout.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Database db;

        public CheckoutController(Database db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            Session session = db.Sessions.FirstOrDefault(x => x.Id ==
              Request.Cookies["sessionId"]);

            //check whether user has logged in
            //if never login, redirect to login page
            if(session==null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                //create a new purchase record
                Purchase pur = new Purchase()
                {
                    Id = Guid.NewGuid().ToString(),
                    Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    UserId = session.UserId,
                };
                db.Purchases.Add(pur);
                db.SaveChanges();

                //retrieve cart details of user
                List<CartDetail> cds = db.CartDetails.Where(x=>x.UserId == session.UserId).ToList();

                //transfer data from cartdetail to purchasedetail
                foreach (CartDetail cd in cds)
                {
                    PurchaseDetail pd = new PurchaseDetail()
                    {
                        ProductId = cd.ProductId,
                        Quantity = cd.Quantity,
                        UserId = session.UserId,
                        PurchaseId = pur.Id,
                    };
                    db.PurchaseDetails.Add(pd);
                    db.SaveChanges();

                    // create activation keys
                    for (int i = 0; i < pd.Quantity; i++)
                    {
                        ActivationKey ak = new ActivationKey()
                        {
                            PurchaseDetailProductId = pd.ProductId,
                            PurchaseDetailPurchaseId = pd.PurchaseId,
                            PdtAtvKey = Guid.NewGuid().ToString(),
                        };
                        db.ActivationKeys.Add(ak);
                        db.SaveChanges();
                    }

                    //delete the record of the cartdetail
                    db.CartDetails.Remove(cd);
                    db.SaveChanges();
                }


                ViewData["sessionId"] = HttpContext.Request.Cookies["sessionId"];

                return RedirectToAction("Index", "MyPurchases");
            }

           
        }
    }
}
