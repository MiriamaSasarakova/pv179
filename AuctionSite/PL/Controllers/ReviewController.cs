﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using BL.DTOs.Base;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;

namespace PL.Controllers
{
    public class ReviewController : BaseController
    {

        public UserFacade UserFacade { get; set; }

        public ReviewFacade ReviewFacade { get; set; }

        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> User(int id)
        {
            var user = await UserFacade.GetUserByIdAsync(id);
            if (user == null)
                return Error();
            TempData["UserEmail"] = user.Email;
            return View(user.Reviews);
        }

        public ActionResult AddReview(int userId)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Denied", "Base");

            }
            return View("Detail",new ReviewDto
            {
                ReviewedUserID = userId,
                UserWhoWroteID = System.Web.HttpContext.Current.User.Identity.GetUserId().AsInt()
            });
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(ReviewDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await (ReviewFacade.AddUserReviewAsync(model));
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public async Task<ActionResult> UpdateReview(int reviewId)
        {           
            var review = await ReviewFacade.GetReviewByIdAsync(reviewId);

            if (review == null || UserId != review.UserWhoWroteID)
            {
                return RedirectToAction("Denied", "Base");
            }
            return View("Detail", review);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateReview(ReviewDto review)
        {
            if (UserId != review.UserWhoWroteID)
            {
                return Denied();
            }

            if (await ReviewFacade.EditUserReview(review))
            {
                return RedirectToAction("Index", "Users");
            }

            return RedirectToAction("Detail", "Review", new {review.Id});
        }

        [HttpGet]
        public async Task<ActionResult> DeleteReview(int reviewId)
        {
            var review = await ReviewFacade.GetReviewByIdAsync(reviewId);
            if (UserId != review.UserWhoWroteID)
            {
                return RedirectToAction("Denied", "Base");
            }
            await (ReviewFacade.DeleteUserReview(review.Id));
            return RedirectToAction("Index", "Users");
        }
    }
}