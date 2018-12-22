using LimonDijital.BusinessLayer;
using LimonDijital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LimonDijital.WebApp.WebApis
{
    public class QuestionController : ApiController
    {
        private QuestionManager questionManager = new QuestionManager();

        public IEnumerable<LimonQuestion> Get()
        {
            return questionManager.List();
        }
    }
}
