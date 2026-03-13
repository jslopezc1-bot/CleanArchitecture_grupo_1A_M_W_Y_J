using CleanArchitecture.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Services.Almacen
{
    public class AlmacenService
    {
        private readonly IAlmacenRepository _almacenRepository;

        public AlmacenService(IAlmacenRepository almacenRepository)
        {
            _almacenRepository = almacenRepository;
        }

        public void AgregarAlmacen(Domain.Entities.Almacen almacen)
        {
            _almacenRepository.Add(almacen);
        }
    }
}
