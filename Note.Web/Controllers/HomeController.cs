using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Note.BusinessLayer.Abstract;
using Note.Web.Models;
using Note.Web.ViewModels;
using System.Diagnostics;

namespace Note.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppNoteService _appNoteService;
        private readonly IMapper _mapper; 

        public HomeController(IAppNoteService appNoteService, IMapper mapper)
        {
            _appNoteService = appNoteService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var values=_appNoteService.TGetAll().ToList();
            return View(_mapper.Map<List<AppNoteListVM>>(values));
        }

        
    }
}