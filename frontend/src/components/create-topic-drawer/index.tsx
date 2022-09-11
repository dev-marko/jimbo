import { Button, Drawer, DrawerBody, DrawerCloseButton, DrawerContent, DrawerFooter, DrawerHeader, DrawerOverlay, FormControl, FormErrorMessage, FormLabel, Input, Stack } from '@chakra-ui/react';
import { QueryObserverResult, useMutation } from '@tanstack/react-query';
import { AxiosResponse } from 'axios';
import { Field, Formik } from 'formik';
import { useRouter } from 'next/router';
import { object, string } from 'yup';

import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { useAuth } from '~providers/auth-provider';
import { Topic } from '~types/topics';

type Props = {
  refetch: () => Promise<QueryObserverResult<AxiosResponse<any, any>, unknown>>;
  drawerRef: any;
  onClose: () => void;
  isOpen: boolean;
};

const CreateTopicDrawer = ({ refetch, drawerRef, onClose, isOpen }: Props) => {
  const { token } = useAuth();
  const { query: { id } } = useRouter();
  const { mutateAsync } = useMutation(
    [`/Forum/sub-forum/${id}/add-topic`],
    async ({ title }: Pick<Topic, 'title'>) => fetcher.post(`${FORUM_API_URL}/Forum/sub-forum/${id}/add-topic`, { title }, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }),
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
          Create a new topic
        </DrawerHeader>

        <DrawerBody>
          <Formik
            initialValues={{
              title: '',
            }}
            validationSchema={object({
              title: string().required('Title is required'),
            })}
            onSubmit={async ({ title }: Pick<Topic, 'title'>) => {
              await mutateAsync({ title });
              await refetch();
              onClose();
            }}
          >
            {({ handleSubmit, errors, touched }) => (
              <form onSubmit={handleSubmit} id="createForum">
                <Stack spacing={6}>
                  <FormControl isInvalid={!!errors.title && touched.title}>
                    <FormLabel htmlFor="title">Title</FormLabel>
                    <Field
                      as={Input}
                      variant="filled"
                      id="title"
                      name="title"
                    />
                    <FormErrorMessage>{errors.title}</FormErrorMessage>
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

export default CreateTopicDrawer;
