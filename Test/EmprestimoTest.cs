using Domain.Enums;
using Domain.Models;
using System;
using Xunit;

namespace Test
{
    public class EmprestimoTest
    {
        [Fact]
        public void IndicaAtrasoParaDevolucao()
        {
            // Arrange
            var emprestimo = new Emprestimo
            {
                DataDevolucao = new DateTime(2022, 1, 20)
            };

            // Act
            var atraso = emprestimo.VerificarAtraso(new DateTime(2022, 1, 21));

            // Assert
            Assert.True(atraso);
        }

        [Fact]
        public void EmprestaReduzindoQuantidadeDisponivel()
        {
            // Arrange
            var ferramenta = new Ferramenta
            {
                QuantidadeDisponivel = 5
            };

            // Act
            ferramenta.Emprestar(3);

            // Assert
            Assert.Equal(2, ferramenta.QuantidadeDisponivel);
        }

        [Fact]
        public void NaoEmprestaSemQuantidadeSuficiente()
        {
            // Arrange
            var ferramenta = new Ferramenta
            {
                QuantidadeDisponivel = 5
            };

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            // Act
            ferramenta.Emprestar(6));
        }

        [Fact]
        public void EmprestarTodaQuantidadeDeixaFerramentaIndisponivel()
        {
            // Arrange
            var ferramenta = new Ferramenta
            {
                QuantidadeDisponivel = 5
            };

            // Act
            ferramenta.Emprestar(5);

            // Assert
            Assert.Equal(StatusFerramentaEnum.Emprestada, ferramenta.Status);
        }

        [Fact]
        public void DevolucaoAumentaQuantidadeDisponivelDeFerramenta()
        {
            // Arrange
            var ferramenta = new Ferramenta
            {
                QuantidadeDisponivel = 2
            };

            // Act
            ferramenta.Devolver(1);

            // Assert
            Assert.Equal(3, ferramenta.QuantidadeDisponivel);
        }

        [Fact]
        public void DevolucaoDeixaFerramentaDisponivel()
        {
            // Arrange
            var ferramenta = new Ferramenta
            {
                QuantidadeDisponivel = 0
            };

            // Act
            ferramenta.Devolver(1);

            // Assert
            Assert.Equal(StatusFerramentaEnum.Disponivel, ferramenta.Status);
        }
    }
}
