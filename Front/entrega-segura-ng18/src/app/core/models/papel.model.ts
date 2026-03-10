export enum Papel {
  ADMIN = 'Administrador',
  SINDICO = 'Sindico',
  FUNCIONARIO = 'Funcionario',
  MORADOR = 'Morador',
}

export function ehPapelValido(papel: string): papel is Papel {
  return Object.values(Papel).includes(papel as Papel);
}
