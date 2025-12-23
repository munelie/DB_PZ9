using AutoMapper;
using LibraryAPI.DTO;
using LibraryAPI.Models;

namespace LibraryAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.Books.Count));

            CreateMap<Author, AuthorSimpleDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Book, BookSimpleDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();

            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.Books.Count));

            CreateMap<Genre, GenreWithBooksDto>()
                .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.Books.Count));

            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Genre, GenreSimpleDto>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<User, UserSimpleDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<Loan, LoanDto>()
                .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => src.ReturnDate.HasValue))
                .ForMember(dest => dest.IsOverdue, opt => opt.MapFrom(src =>
                    src.ReturnDate == null && src.DueDate < DateTime.UtcNow));

            CreateMap<CreateLoanDto, Loan>();
            CreateMap<UpdateLoanDto, Loan>();
        }
    }
}