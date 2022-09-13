/* eslint-disable react/jsx-no-constructed-context-values */
/* eslint-disable import/no-anonymous-default-export */
import { useRouter } from 'next/router';
import { createContext, useContext, useEffect, useState } from 'react';

import { AUTHENTICATION_API_URL } from '~constants/api';
import { TOKEN, USER } from '~constants/storage';
import fetcher from '~fetcher';
import { SignInInput } from '~types/authentication/sign-in';
import { SignUpInput } from '~types/authentication/sign-up';
import { readFromLocalStorage, removeFromLocalStorage, writeToLocalStorage } from '~utils/local-storage';

const publicRoutes: string[] = ['/sign-up', '/sign-in'];

type Props = {
  children: React.ReactNode;
};

type AuthContextProps = {
  user: string | null;
  token: string | null;
  signIn: (input: SignInInput) => Promise<void>;
  signUp: (input: SignUpInput) => Promise<void>;
  signOut: () => void;
  isAuthenticated: boolean;
};

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider = ({ children }: Props) => {
  const { push, asPath, pathname } = useRouter();
  const [token, setToken] = useState<string | null>(readFromLocalStorage(TOKEN));
  const [user, setUser] = useState<string | null>(readFromLocalStorage(USER));
  const isAuthenticated = !!(token && user);

  useEffect(() => {
    const isOnAuthenticatedRoute = !publicRoutes.some((route: string) => route === asPath);

    if (isAuthenticated && !isOnAuthenticatedRoute) {
      push('/');
    } else if (!isAuthenticated && isOnAuthenticatedRoute) {
      push('/sign-in');
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pathname]);

  const signUp = async ({ username, password, email }: SignUpInput) => {
    await fetcher.post(`${AUTHENTICATION_API_URL}/register`, {
      username,
      password,
      email,
    });

    setInterval(() => {
      push({ pathname: '/sign-in', query: { email } });
    }, 1000);
  };

  const signIn = async ({ username, password }: SignInInput) => {
    const { data: { username: loggedInUserUsername, token: loggedInUserToken },
    } = await fetcher.post(`${AUTHENTICATION_API_URL}/login`, {
      username,
      password,
    });

    setToken(loggedInUserToken);
    setUser(loggedInUserUsername);
    writeToLocalStorage(TOKEN, loggedInUserToken);
    writeToLocalStorage(USER, loggedInUserUsername);

    push('/');
  };

  const signOut = async () => {
    setToken(null);
    setUser(null);
    removeFromLocalStorage(TOKEN);
    removeFromLocalStorage(USER);

    push('/sign-in');
  };

  return (
    <AuthContext.Provider value={{
      isAuthenticated,
      signUp,
      signIn,
      signOut,
      user,
      token,
    }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};

export default {
  AuthProvider,
  useAuth,
};
