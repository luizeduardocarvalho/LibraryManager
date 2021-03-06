﻿using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Entities.Dtos;
using System.Collections.Generic;

namespace LibraryManager.Domain.Services.Contracts
{
    public interface IBookService
    {
        IList<Book> GetAll();
        Book GetById(int id);
        Book Create(CreateBookDto book);
    }
}
