/* eslint-disable import/no-anonymous-default-export */
import { createContext, PropsWithChildren, useContext, useEffect, useState } from "react";
import { useToast } from '@chakra-ui/react';
import { useRouter } from "next/router";

import phaxios from "~phaxios";
import { SignInInput } from "~types/authentication/sign-in";
import { TOKEN, USER } from "~constants/storage";
import { isOnAuthenticatedRoute } from "~utils/is-on-authenticated-route";
import { SignUpInput } from "~types/authentication/sign-up";
import { readFromLocalStorage, removeFromLocalStorage, writeToLocalStorage } from "~utils/local-storage";

type Props = PropsWithChildren<{}>;

type AuthContextProps = {
    user: string | null;
    signIn: (input: SignInInput) => Promise<void>;
    signUp: (input: SignUpInput) => Promise<void>;
    signOut: () => void;
    isAuthenticated: boolean;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined)


export const AuthProvider = ({ children }: Props) => {
    const { push, replace, asPath, pathname } = useRouter();
    const toast = useToast();
    const [token, setToken] = useState<string | null>(readFromLocalStorage(TOKEN));
    const [user, setUser] = useState<string | null>(readFromLocalStorage(USER));
    const isAuthenticated = !!(token && user);

    useEffect(() => {
        if ((isAuthenticated && isOnAuthenticatedRoute(asPath)) || (!isAuthenticated && !isOnAuthenticatedRoute(asPath))) {
            replace(asPath);
        } else if (isAuthenticated && !isOnAuthenticatedRoute(asPath)) {
            replace("/");
        } else {
            replace("/sign-in");
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [pathname])

    const signUp = async ({ username, password, email }: SignUpInput) => {
        await phaxios.post("/register", {
            username,
            password,
            email,
        });

        push(`/sign-in?username=${username}`);
    }

    const signIn = async ({ username, password }: SignInInput) => {
        try {
            const { data } = await phaxios.post("/login", {
                username,
                password,
            })

            setToken(data.token);
            writeToLocalStorage(TOKEN, data.token);
            setUser(data.username);
            writeToLocalStorage(USER, data.username);

            push("/");
        } catch (error) {
            toast({
                title: "Error",
                description: error as string ?? "Something went wrong",
                status: "error",
                isClosable: true,
                duration: null,
                position: "bottom-right",
            })
        }
    }

    const signOut = async () => {
        setToken(null);
        removeFromLocalStorage(TOKEN);
        setUser(null);
        removeFromLocalStorage(USER);

        push("/sign-in");
    }


    return (
        <AuthContext.Provider value={{
            isAuthenticated,
            signUp,
            signIn,
            signOut,
            user,
        }}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
}

export default {
    AuthProvider,
    useAuth
};