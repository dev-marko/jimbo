import { Button, FormControl, FormErrorMessage, FormLabel, Heading, Input, Stack, Textarea, VStack } from '@chakra-ui/react';
import { Field, Formik } from 'formik';
import { ReactElement } from 'react';
import { object, string } from 'yup';

import AuthenticatedLayout from '~components/authenticated-layout';

const Create = () => {
  return (
    <VStack w="full" align="flex-start">
      <Heading size="md">
        New workout plan

        <Formik
          initialValues={{
            name: '',
            description: '',
            category: '',
          }}
          validationSchema={object({
            name: string().required('Name is required'),
            description: string().required('Description is required'),
          })}
          onSubmit={async (values) => {
            console.log(values);
          }}
        >
          {({ handleSubmit, errors, touched }) => (
            <form onSubmit={handleSubmit} id="createForum">
              <Stack spacing={6} mt={12}>
                <FormControl isInvalid={!!errors.name && touched.name}>
                  <FormLabel htmlFor="name">Name</FormLabel>
                  <Field
                    as={Input}
                    variant="filled"
                    id="name"
                    name="name"
                  />
                  <FormErrorMessage>{errors.name}</FormErrorMessage>
                </FormControl>

                <FormControl isInvalid={!!errors.description && touched.description}>
                  <FormLabel htmlFor="description">Description</FormLabel>
                  <Field as={Textarea} variant="filled" name="description" id="description" resize="none" />
                  <FormErrorMessage>{errors.description}</FormErrorMessage>
                </FormControl>

                <Button colorScheme="purple" w="max-content">
                  Add week
                </Button>
              </Stack>
            </form>
          )}
        </Formik>
      </Heading>
    </VStack>
  );
};

Create.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default Create;
