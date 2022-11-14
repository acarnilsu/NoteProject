using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Note.BusinessLayer.Abstract;
using Note.EntityLayer.Concrete;

namespace Note.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class NoteController : Controller
    {
        private readonly IAppNoteService _appNoteService;
        private readonly IMapper _mapper;

        public NoteController(IAppNoteService appNoteService, IMapper mapper)
        {
            _appNoteService = appNoteService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var values=_appNoteService.TGetAll().ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppNote appNote)
        {
            appNote.ID = Guid.NewGuid().ToString();
            appNote.CreatedDate= DateTime.Now;
            appNote.ModifiedDate= DateTime.Now;
            await _appNoteService.TAddAsync(appNote);
            return RedirectToAction("Index");
        }


        public  async Task<IActionResult> Delete(string id)
        {
            var values=await _appNoteService.TGetByIdAsync(id);
            _appNoteService.TRemove(values);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var values = await _appNoteService.TGetByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AppNote appNote)
        {
            appNote.ModifiedDate = DateTime.Now;
            _appNoteService.TUpdate(appNote);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(string id)
        {
            var values = await _appNoteService.TGetByIdAsync(id);
            return View(values);
        }
    }
}
