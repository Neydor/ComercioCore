export interface LoginCredentials {
  email: string;
  password: string;
  acceptTerms: boolean;
}

export interface AuthResponse {
  token: string;
  user: {
    id: string;
    email: string;
    name: string;
  };
}
