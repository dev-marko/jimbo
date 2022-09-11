import { Box, Divider, HStack, Icon, IconButton, Text, VStack } from '@chakra-ui/react';
import { useMutation, useQuery } from '@tanstack/react-query';
import { useRouter } from 'next/router';
import { FaTrash } from 'react-icons/fa';
import { format } from 'timeago.js';

import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { useAuth } from '~providers/auth-provider';
import { Post } from '~types/posts';

type Props = Post;

const PostCard = ({ postId, authorUsername, content, createdAt }: Props) => {
  const { user } = useAuth();
  const isPostAuthor = authorUsername === user;
  const { query: { topicId } } = useRouter();

  const { mutateAsync, isLoading } = useMutation([`/Post/${postId}`], async () => fetcher.delete(`${FORUM_API_URL}/Post/${postId}`));

  const { refetch } = useQuery([`/Topic/${topicId}`], async () => fetcher.get(`${FORUM_API_URL}/Topic/${topicId}`), {
    enabled: !!topicId,
  });

  const handleDelete = async () => {
    await mutateAsync();
    await refetch();
  };

  return (
    <VStack spacing={2} w="full" p={{ base: 0, md: 4 }} rounded="lg">
      <HStack w="full" justify="space-between">
        <Box>
          <Text color="gray.600" alignSelf="start" size="sm">
            {authorUsername}
            {' '}
            says:
          </Text>
          <Text wordBreak="break-word">
            {content}
          </Text>
        </Box>
        <Box>
          {isPostAuthor && (
            <IconButton onClick={handleDelete} isDisabled={isLoading} size="xs" variant="ghost" aria-label="Delete post button" icon={<Icon fontSize="md" as={FaTrash} color="red.500" />} />
          )}
        </Box>
      </HStack>
      <HStack w="full" justify="end">
        <Text color="gray.400" flexShrink={0}>
          {format(createdAt)}
        </Text>
      </HStack>
      <Divider borderWidth="1px" rounded="lg" />
    </VStack>
  );
};

export default PostCard;
