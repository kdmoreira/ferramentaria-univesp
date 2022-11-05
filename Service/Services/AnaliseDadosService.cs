using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AnaliseDadosService : IAnaliseDadosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnaliseDadosService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task GerarMassaDados()
        {
            var adminID = Guid.Parse("bb9ac2c8-c7d4-4c21-b6cf-84419b12a810");
            var generator = new Random();

            // Cadastro Colaboradores
            var colaboradores = new List<Colaborador>();
            for (int i = 0; i < 30; i++)
            {
                var cpf = generator.Next(0, 1000000).ToString("D11");

                var colaborador = new Colaborador(Guid.NewGuid(), DateTime.Now, cpf, cpf, "Fulano", "da Silva " + i.ToString(),
                    "fulano" + i + "@gmail.com", cpf, "Técnico Manutenção", "FURNAS", Perfil(i), true);

                colaboradores.Add(colaborador);

                await _unitOfWork.ColaboradorRepository.AddAsync(colaborador, adminID);
            }

            // Cadastro Ferramentas
            var categoriasIds = new List<Guid>()
            {
                new Guid("5138f09b-7dc6-4e06-a983-c182e6d7d173"),
                new Guid("61858a59-e022-4ace-8531-8db2d62b739e"),
                new Guid("70daebeb-22d5-4b70-8052-9211b92b9552"),
                new Guid("1d16e4d4-59ac-438f-b484-fa097d3be8f3")
            };

            var ferramentas = new List<Ferramenta>();
            var categoriasCount = categoriasIds.Count;
            for (int i = 0; i < 50; i++)
            {
                var quantidadeTotal = generator.Next(30, 61);
                var valorCompra = generator.Next(5, 101);
                var localizacao = generator.Next(1, 6);
                var index = (i % categoriasCount);

                var ferramenta = new Ferramenta()
                {
                    Ativo = true,
                    CategoriaID = categoriasIds[index],
                    Codigo = i.ToString("D3"),
                    NumeroPatrimonial = i.ToString(),
                    Descricao = "Ferramenta " + i,
                    Fabricante = "Best Tools",
                    QuantidadeTotal = quantidadeTotal,
                    QuantidadeDisponivel = quantidadeTotal,
                    ValorCompra = valorCompra,
                    Localizacao = localizacao.ToString() + "A"
                };

                ferramentas.Add(ferramenta);

                await _unitOfWork.FerramentaRepository.AddAsync(ferramenta, adminID);
            }

            // Cadastro Emprestimos
            var emprestimos = new List<Emprestimo>();
            var colaboradoresCount = colaboradores.Count;
            var ferramentasCount = ferramentas.Count;
            for (int i = 0; i < 100; i++)
            {
                var prazo = generator.Next(7, 22);
                var quantidade = generator.Next(1, 4);
                var dataEmprestimo = new DateTime(2022, generator.Next(1, 13), generator.Next(1, 29));
                var colaboradorIndex = generator.Next(0, colaboradoresCount);
                var ferramentaIndex = generator.Next(0, ferramentasCount);

                var emprestimo = new Emprestimo()
                {
                    Ativo = true,
                    ColaboradorID = colaboradores[colaboradorIndex].ID,
                    FerramentaID = ferramentas[ferramentaIndex].ID,
                    DataEmprestimo = dataEmprestimo,
                    DataDevolucao = dataEmprestimo.AddDays(prazo),
                    Quantidade = quantidade
                };

                emprestimos.Add(emprestimo);

                await _unitOfWork.EmprestimoRepository.AddAsync(emprestimo, adminID);
            }

            await _unitOfWork.CommitAsync();

            // Deduzir quantidade disponivel ferramentas

            foreach (var emprestimo in emprestimos)
            {
                var ferramenta = await _unitOfWork.FerramentaRepository.FindByAsync(x => x.ID == emprestimo.FerramentaID);
                ferramenta.Emprestar(emprestimo.Quantidade);
                _unitOfWork.FerramentaRepository.Update(ferramenta, x => x.ID == ferramenta.ID, adminID);
            }

            await _unitOfWork.CommitAsync();
        }

        private PerfilEnum Perfil(int i)
        {
            if (i % 5 == 0)
            {
                return PerfilEnum.Supervisor;
            }
            else
            {
                return PerfilEnum.Colaborador;
            }
        }
    }
}
