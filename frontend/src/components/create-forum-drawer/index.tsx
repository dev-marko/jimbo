import { Button, Drawer, DrawerBody, DrawerCloseButton, DrawerContent, DrawerFooter, DrawerHeader, DrawerOverlay, FormControl, FormErrorMessage, FormLabel, Input, Select, Stack, Textarea } from '@chakra-ui/react';
import { QueryObserverResult, useMutation } from '@tanstack/react-query';
import { AxiosResponse } from 'axios';
import { Field, Formik } from 'formik';
import { mixed, object, string } from 'yup';

import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { CategoriesEnum } from '~types/forums/categories';
import { Forum } from '~types/forums/forum';

type Props = {
  refetch: () => Promise<QueryObserverResult<AxiosResponse<any, any>, unknown>>;
  drawerRef: any;
  onClose: () => void;
  isOpen: boolean;
};

const CreateForumDrawer = ({ refetch, drawerRef, onClose, isOpen }: Props) => {
  const { mutateAsync } = useMutation(
    ['/Forum/sub-forum'],
    async (forum: Omit<Forum, 'id' | 'topics'>) => fetcher.post(`${FORUM_API_URL}/Forum/sub-forum`, forum),
  );

  return (
    <Drawer
      isOpen={isOpen}
      placement="right"
      initialFocusRef={drawerRef}
      onClose={onClose}
    >
      <DrawerOverlay />
      <DrawerContent>
        <DrawerCloseButton />
        <DrawerHeader borderBottomWidth="1px">
          Create a new forum
        </DrawerHeader>

        <DrawerBody>
          <Formik
            initialValues={{
              name: '',
              description: '',
              category: '',
            }}
            validationSchema={object({
              name: string().required('Name is required'),
              description: string().required('Description is required'),
              categories: mixed().oneOf(Object.keys(CategoriesEnum)),
            })}
            onSubmit={async (values: Omit<Forum, 'id' | 'topics'>) => {
              const { name, description, category } = values;

              await mutateAsync({ name, description, category });
              await refetch();
              onClose();
            }}
          >
            {({ handleSubmit, errors, touched }) => (
              <form onSubmit={handleSubmit} id="createForum">
                <Stack spacing={6}>
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

                  <FormControl isInvalid={!!errors.category && touched.category}>
                    <FormLabel htmlFor="category">Select Category</FormLabel>
                    <Field as={Select} name="category" id="category">
                      {
                        Object.keys(CategoriesEnum).map((key) => (
                          <option key={key} value={key}>{CategoriesEnum[key]}</option>
                        ))
                      }
                    </Field>
                    <FormErrorMessage>{errors.category}</FormErrorMessage>
                  </FormControl>
                </Stack>
              </form>
            )}
          </Formik>
        </DrawerBody>

        <DrawerFooter borderTopWidth="1px">
          <Button variant="outline" mr={3} onClick={onClose}>
            Cancel
          </Button>
          <Button colorScheme="purple" type="submit" form="createForum">Submit</Button>
        </DrawerFooter>
      </DrawerContent>
    </Drawer>
  );
};

export default CreateForumDrawer;
