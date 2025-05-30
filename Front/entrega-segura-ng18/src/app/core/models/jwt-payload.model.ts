export interface JwtPayload {
  Id: string;
  Nome: string;
  Email: string;
  Perfil: string;
  Foto?: string;
  exp: number;
  iat: number;
}
