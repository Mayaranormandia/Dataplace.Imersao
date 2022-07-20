using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using Dataplace.Imersao.Core.Tests.Fixtures;
using System;
using Xunit;

namespace Dataplace.Imersao.Core.Tests.Domain.Orcamentos
{
    [Collection(nameof(OrcamentoCollection))]
    public class OrcamentoTest
    {
        private readonly OrcamentoFixture _fixture;
        public OrcamentoTest(OrcamentoFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void NovoOrcamentoDevePossuirValoresValidos()
        {
            // Organizar Ato
            var orcamento = _fixture.NovoOrcamento();


            // Afirmar
            Assert.True(orcamento.CdEmpresa == _fixture.CdEmpresa);
            Assert.True(orcamento.CdFilial == _fixture.CdFilial);
            Assert.Equal(_fixture.NumOrcaemtp, orcamento.NumOrcamento);
            Assert.True(orcamento.Cliente.Codigo == _fixture.Cliente.Codigo);
            Assert.True(OrcamentoUsuario.Usuario == _fixture.UserName);
            Assert.True(OrcamentoUsuario.Usuario == _fixture.UserName);
            Assert.True(orcamento.Situacao == Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Aberto);
            Assert.Null(orcamento.Validade);
            Assert.NotNull(orcamento.TabelaPreco);
            Assert.Equal(_fixture.TavelaPreco.CdTabela, orcamento.TabelaPreco.CdTabela);
            Assert.Equal(_fixture.TavelaPreco.SqTabela, orcamento.TabelaPreco.SqTabela);
        }
        [Fact]
        public void FecharOrcamentoDeveRetornarStatusFechado()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();


            // Act
            orcamento.FecharOrcamento();


            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Fechado, orcamento.Situacao);
            Assert.NotNull(orcamento.DtFechamento);
        }


        [Fact]
        public void TentarFecharOrcamentoJaFechadoRetornarException()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.FecharOrcamento();

            // act & assert
            Assert.Throws<DomainException>(() => orcamento.FecharOrcamento());
        }
        [Fact]
        public void ReabrirOrcamentoTeste()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.FecharOrcamento();

            // act & assert
            Assert.True(orcamento.Situacao == Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Fechado);

        }
        [Fact]
        public void ValidadeTeste()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();

            // act & assert
            Assert.True(orcamento.Situacao == Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Aberto);
            Assert.Equal(orcamento.Validade, OrcamentoValidade.dias(17));

        }
        [Fact]
        public void CancelamentoItemTeste()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.FecharOrcamento();

            // act & assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Fechado, orcamento.Situacao);
            Assert.NotNull(orcamento.DtFechamento);
        }

        [Fact]
        public void InsercaoItemOrcamentoTeste()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();


            // act & assert

            Assert.Null(orcamento.Quantidade);
            Assert.Equal(_fixture.TavelaPreco.CdTabela, orcamento.TabelaPreco.CdTabela);
        }
    }
}
