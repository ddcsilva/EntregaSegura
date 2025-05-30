import { MatPaginatorIntl } from '@angular/material/paginator';

export function obterPaginatorIntlPortugues() {
    const paginatorIntl = new MatPaginatorIntl();

    paginatorIntl.itemsPerPageLabel = 'Itens por página:';
    paginatorIntl.nextPageLabel = 'Próxima página';
    paginatorIntl.previousPageLabel = 'Página anterior';
    paginatorIntl.firstPageLabel = 'Primeira página';
    paginatorIntl.lastPageLabel = 'Última página';

    paginatorIntl.getRangeLabel = (pagina: number, quantidadePaginas: number, tamanho: number) => {
        if (tamanho === 0 || quantidadePaginas === 0) {
            return `0 de ${tamanho}`;
        }

        tamanho = Math.max(tamanho, 0);
        const startIndex = pagina * quantidadePaginas;
        const endIndex = startIndex < tamanho
            ? Math.min(startIndex + quantidadePaginas, tamanho)
            : startIndex + quantidadePaginas;
        return `${startIndex + 1} - ${endIndex} de ${tamanho}`;
    };

    return paginatorIntl;
}
