/* eslint-disable react/jsx-no-constructed-context-values */
/* eslint-disable import/no-anonymous-default-export */
import { createContext, useContext, useEffect, useState } from 'react';
import { useRouter } from 'next/router';

import { SignInInput } from '~types/authentication/sign-in';
import { TOKEN, USER } from '~constants/storage';
import { isOnAuthenticatedRoute } from '~utils/is-on-authenticated-route';
import { SignUpInput } from '~types/authentication/sign-up';
import { readFromLocalStorage, removeFromLocalStorage, writeToLocalStorage } from '~utils/local-storage';
import fetcher from '~fetcher';
import { AUTHENTICATION_API_URL } from '~constants/api';

type Props = {
  children: React.ReactNode;
};

type AuthContextProps = {
  user: string | null;
  signIn: (input: SignInInput) => Promise<void>;
  signUp: (input: SignUpInput) => Promise<void>;
  signOut: () => void;
  isAuthenticated: boolean;
};

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider = ({ children }: Props) => {
  const { push, replace, asPath, pathname } = useRouter();
  const [token, setToken] = useState<string | null>(readFromLocalStorage(TOKEN));
  const [user, setUser] = useState<string | null>(readFromLocalStorage(USER));
  const isAuthenticated = !!(token && user);

  useEffect(() => {
    if ((isAuthenticated && isOnAuthenticatedRoute(asPath)) ||
      (!isAuthenticated && !isOnAuthenticatedRoute(asPath))) {
      replace(asPath);
    } else if (isAuthenticated && !isOnAuthenticatedRoute(asPath)) {
      replace('/');
    } else {
      replace('/sign-in');
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
