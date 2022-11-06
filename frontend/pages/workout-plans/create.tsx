import { Button, FormControl, FormErrorMessage, FormLabel, Heading, Input, Stack, Textarea, VStack } from '@chakra-ui/react';
import { Field, Formik, useFormik } from 'formik';
import { ReactElement } from 'react';
import { object, string } from 'yup';

import AuthenticatedLayout from '~components/authenticated-layout';

const Create = () => {
  const formik = useFormik({
    initialValues: {
      name: '',
      description: '',
    },
    validationSchema: object({
      name: string().required('Name is required'),
      description: string().required('Description is required'),
    }),
    onSubmit: (values) => {
      alert(JSON.stringify(values, null, 2));
    },
  });

  return (
    <VStack w="full" align="flex-start">
      <Heading size="md">
        New workout plan
      </Heading>
      <form onSubmit={formik.handleSubmit}>
        <Stack spacing={6} mt={12}>
          <FormControl isInvalid={!!formik.errors.name && formik.touched.name}>
            <FormLabel htmlFor="name">Name</FormLabel>
            <Input
              variant="filled"
              id="name"
              name="name"
              onChange={formik.handleChange}
              value={formik.values.name}
            />
            <FormErrorMessage>{formik.errors.name}</FormErrorMessage>
          </FormControl>

          <FormControl isInvalid={!!formik.errors.description && formik.touched.description}>
            <FormLabel htmlFor="description">Description</FormLabel>
            <Textarea
              onChange={formik.handleChange}
              variant="filled"
              name="description"
              id="description"
              resize="none"
              value={formik.values.description}
            />
            <FormErrorMessage>{formik.errors.description}</FormErrorMessage>
          </FormControl>

          <Button type="submit" colorScheme="purple" w="max-content">
            Add week
          </Button>
        </Stack>
      </form>
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
