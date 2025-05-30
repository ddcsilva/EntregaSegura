import { User } from './user.model';

export interface LoginRequest {
  login: string;
  senha: string;
}

export interface AuthResponse {
  token: string;
  user: User;
  expires?: string;
}

export interface AuthState {
  user: User | null;
  token: string | null;
  isLoading: boolean;
  error: string | null;
}

export interface JwtPayload {
  Id: string;
  Nome: string;
  Email: string;
  Perfil: string;
  Foto?: string;
  exp: number;
  iat: number;
}
