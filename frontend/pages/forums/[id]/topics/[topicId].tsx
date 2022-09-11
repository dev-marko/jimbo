import { Box, Button, chakra, Divider, FormControl, FormErrorMessage, FormLabel, Heading, HStack, Icon, IconButton, Input, Modal, Popover, PopoverArrow, PopoverBody, PopoverCloseButton, PopoverContent, PopoverHeader, PopoverTrigger, Spinner, Stack, Text, VStack } from '@chakra-ui/react';
import { useMutation, useQuery } from '@tanstack/react-query';
import { Formik, Field } from 'formik';
import { useRouter } from 'next/router';
import { ReactElement, useEffect, useState } from 'react';
import { format } from 'timeago.js';
import { object, string } from 'yup';
import { BiSend } from 'react-icons/bi';
import dynamic from 'next/dynamic';

import AuthenticatedLayout from '~components/authenticated-layout';
import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { Post } from '~types/posts';
import { Topic } from '~types/topics';
import { useAuth } from '~providers/auth-provider';

const EmojiPicker = dynamic(
  () => import('emoji-picker-react'),
  { ssr: false },
);

const SubTopic = () => {
  const [topic, setTopic] = useState<Topic | undefined>(undefined);
  const { token } = useAuth();
  const { query: { topicId } } = useRouter();
  const { data, isLoading, refetch } = useQuery([`/Topic/${topicId}`], async () => fetcher.get(`${FORUM_API_URL}/Topic/${topicId}`), {
    enabled: !!topicId,
  });
  const { mutateAsync, isLoading: isLoadingAddPost } = useMutation([`/Topic/${topicId}/add-post`], async ({ content }: Pick<Post, 'content'>) => fetcher.post(`${FORUM_API_URL}/Topic/${topicId}/add-post`, { content }, { headers: {
    Authorization: `Bearer ${token}`,
  } }));

  useEffect(() => {
    if (data) {
      setTopic(data.data);
    }
  }, [data]);

  if (isLoading) {
    return (
      <Spinner />
    );
  }

  return (
    <>
      <VStack w="full" align="start" spacing={8} pb={32}>
        <VStack spacing={4}>
          <Heading size="lg">
            Topic:
            {' '}
            <chakra.span textDecor="underline" textUnderlineOffset={10}>{topic?.title}</chakra.span>
          </Heading>
          <Heading size="sm" fontWeight="normal" color="gray.700">
            Created by
            {' '}
            {topic?.ownerUsername}
          </Heading>
        </VStack>
        {topic?.posts.map((post: Post) => (
          <VStack key={post.postId} spacing={4} w="full" p={{ base: 0, md: 4 }} rounded="lg">
            <Text color="gray.600" alignSelf="start" size="sm">
              {post.authorUsername}
              {' '}
              says:
            </Text>
            <HStack key={post.postId} w="full" justify="space-between" wordBreak="break-word">
              <Text>
                {post.content}
              </Text>
              <Text color="gray.400" flexShrink={0}>
                {format(post.createdAt)}
              </Text>
            </HStack>
            <Divider borderWidth="1px" rounded="lg" />
          </VStack>
        ))}
      </VStack>
      <Box pos="fixed" bottom={0} p={6} bg="purple.200" flexWrap="nowrap" roundedTop="lg">
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
                    placeholder={`Say something in ${topic?.title}`}
                    isDisabled={isLoadingAddPost}
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
                <IconButton isLoading={isLoadingAddPost} type="submit" aria-label="Add post button" icon={<Icon as={BiSend} />} />
              </HStack>
            </form>
          )}
        </Formik>
      </Box>
    </>
  );
};

SubTopic.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default SubTopic;
