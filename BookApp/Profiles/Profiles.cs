using AutoMapper;
using BookApp.DTOs.RequestDTOs;
using BookApp.Entities;

namespace BookApp.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<BookWithAuthorsRequestDTO, Book>().ForMember(b => b.Users, a => a.MapFrom(s => s.AuthorRequestDTOs));
            CreateMap<Book, BookWithAuthorsRequestDTO>().ForMember(b => b.AuthorRequestDTOs, a => a.MapFrom(s => s.Users));
            // .ForMember(s => s.AuthorIds, c => c.MapFrom(a => a.Authors));
            //CreateMap<BookRequestDTO, Book>().ForMember(b => b.Authors, a => a.MapFrom(s => s.AuthorRequestDTOs));
            //CreateMap<Book, BookRequestDTO>().ForMember(b => b.AuthorRequestDTOs, a => a.MapFrom(s => s.Authors));
            CreateMap<Author, AuthorRequestDTO>().ForMember(a => a.BookRequestDTOs, b => b.MapFrom(s => s.Books));
            CreateMap<AuthorRequestDTO, Author>().ForMember(b => b.Books, a => a.MapFrom(s => s.BookRequestDTOs));


            CreateMap<User, RegisterUserRequestDTO>();
            CreateMap<RegisterUserRequestDTO, User>();


            CreateMap<BookWithManyAuthorsRequestDTO, Book>();
            CreateMap<Book, BookWithManyAuthorsRequestDTO>();

            //CreateMap<OrderBookRequestDTO, OrderBook>();
            //CreateMap<OrderBook, OrderBookRequestDTO>();
        }
    }
}
