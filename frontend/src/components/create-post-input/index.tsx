import { Box, Button, FormControl, HStack, Icon, IconButton, Input, Popover, PopoverArrow, PopoverContent, PopoverTrigger } from '@chakra-ui/react';
import { useMutation, useQuery } from '@tanstack/react-query';
import { Field, Formik } from 'formik';
import dynamic from 'next/dynamic';
import { useRouter } from 'next/router';
import { BiSend } from 'react-icons/bi';
import { object, string } from 'yup';

import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { useAuth } from '~providers/auth-provider';
import { Post } from '~types/posts';

const EmojiPicker = dynamic(
  () => import('emoji-picker-react'),
  { ssr: false },
);

const CreatePostInput = () => {
  const { token } = useAuth();
  const { query: { topicId } } = useRouter();

  const { mutateAsync, isLoading } = useMutation([`/Topic/${topicId}/add-post`], async ({ content }: Pick<Post, 'content'>) => fetcher.post(`${FORUM_API_URL}/Topic/${topicId}/add-post`, { content }, { headers: {
    Authorization: `Bearer ${token}`,
  } }));

  const { data, refetch } = useQuery([`/Topic/${topicId}`], async () => fetcher.get(`${FORUM_API_URL}/Topic/${topicId}`), {
    enabled: !!topicId,
  });

  return (
    <Box pos="fixed" bottom={0} p={2} bg="purple.200" flexWrap="nowrap" roundedTop="md">
      <Formik
        initialValues={{
          content: '',
        }}
        validationSchema={object({
          content: string().required('You need to add some content'),
        })}
        onSubmit={async ({ content }: Pick<Post, 'content'>, { resetForm }) => {
          await mutateAsync({ content });
          await refetch();
          resetForm();
        }}
      >
        {({ handleSubmit, setFieldValue, values }) => (
          <form onSubmit={handleSubmit} id="createForum">
            <HStack spacing={2}>
              <FormControl>
                <Field
                  id="content"
                  name="content"
                  as={Input}
                  w="full"
                  variant="filled"
                  _focus={{
                    bg: 'whiteAlpha.700',
                    borderColor: 'purple.500',
                    borderWidth: '2px',
                  }}
                  placeholder={`Say something in ${data?.data?.title}`}
                  isDisabled={isLoading}
                />
              </FormControl>
              <Popover>
                <PopoverTrigger>
                  <Button>
                    😃
                  </Button>
                </PopoverTrigger>
                <PopoverContent width="auto">
                  <PopoverArrow />
                  <EmojiPicker onEmojiClick={(
                    _event: React.MouseEvent,
                    { emoji }: { emoji: string },
                  ) => {
                    setFieldValue('content', `${values.content}${emoji}`);
                  }}
                  />
                </PopoverContent>
              </Popover>
              <IconButton isLoading={isLoading} type="submit" aria-label="Add post button" icon={<Icon as={BiSend} />} />
            </HStack>
          </form>
        )}
      </Formik>
    </Box>
  );
};

export default CreatePostInput;
