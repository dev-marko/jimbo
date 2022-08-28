import { ChakraProvider } from '@chakra-ui/react'
import type { NextPage } from 'next'
import type { AppProps } from 'next/app'
import type { ReactElement, ReactNode } from 'react'

import '../styles/globals.css'
import { AuthProvider } from '~providers/auth-provider'


export type NextPageWithLayout<P = {}, IP = P> = NextPage<P, IP> & {
  getLayout?: (page: ReactElement) => ReactNode
}

type AppPropsWithLayout = AppProps & {
  Component: NextPageWithLayout
}

function App({ Component, pageProps }: AppPropsWithLayout) {
  const getLayout = Component.getLayout ?? ((page) => page)


  return (
    <ChakraProvider>
      <AuthProvider>
        {getLayout(
          <Component {...pageProps} />
        )}
      </AuthProvider>
    </ChakraProvider>
  )
}

export default App;
