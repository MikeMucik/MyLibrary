using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.BookInfo;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Services
{
	public class BookInfoService : IBookInfoService
	{
		private readonly IBookInfoRepo _bookInfoRepo;
		private readonly IMapper _mapper;
		public BookInfoService(IBookInfoRepo bookInfoRepo,
						IMapper mapper)
		{
			_bookInfoRepo = bookInfoRepo;
			_mapper = mapper;
		}
		public int AddInfoBook(NewBookInfoVm model)
		{
			var newBookInfo = _mapper.Map<BookInfo>(model);
			var toAddedBookInfo = _bookInfoRepo.AddBookInfo(newBookInfo);
			return toAddedBookInfo;
		}
	}
}
