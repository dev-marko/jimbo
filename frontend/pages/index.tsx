import { Box, Heading } from '@chakra-ui/react';
import { ReactElement } from 'react';

import AuthenticatedLayout from '~components/authenticated-layout';

import { NextPageWithLayout } from './_app';

const Home: NextPageWithLayout = () => {
  return (
    <Box>
      <Heading>
        Hello!
      </Heading>
    </Box>
  );
};

Home.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default Home;
