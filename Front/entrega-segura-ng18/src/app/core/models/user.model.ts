export interface User {
  id: number;
  nome: string;
  email: string;
  perfil: UserRole;
  foto?: string;
}

export enum UserRole {
  ADMIN = 'Administrador',
  SINDICO = 'Sindico',
  FUNCIONARIO = 'Funcionario',
  MORADOR = 'Morador',
}

export function isValidUserRole(role: string): role is UserRole {
  return Object.values(UserRole).includes(role as UserRole);
}
