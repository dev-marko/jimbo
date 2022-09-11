import { chakra, Heading, Spinner, VStack } from '@chakra-ui/react';
import { useQuery } from '@tanstack/react-query';
import { useRouter } from 'next/router';
import { ReactElement, useEffect, useState } from 'react';

import AuthenticatedLayout from '~components/authenticated-layout';
import CreatePostInput from '~components/create-post-input';
import PostCard from '~components/post-card';
import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { Post } from '~types/posts';
import { Topic } from '~types/topics';

const SubTopic = () => {
  const [topic, setTopic] = useState<Topic | undefined>(undefined);
  const { query: { topicId } } = useRouter();

  const { data, isLoading } = useQuery([`/Topic/${topicId}`], async () => fetcher.get(`${FORUM_API_URL}/Topic/${topicId}`), {
    enabled: !!topicId,
  });

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
        <VStack spacing={4} align="start">
          <Heading size="lg" fontWeight="normal">
            Topic:
            {' '}
            <chakra.span fontWeight="bold">{topic?.title}</chakra.span>
          </Heading>
          <Heading size="sm" fontWeight="normal" color="gray.700">
            Created by
            {' '}
            {topic?.ownerUsername}
          </Heading>
        </VStack>
        {topic?.posts.map((post: Post) => (
          <PostCard key={post.postId} {...post} />
        ))}
      </VStack>
      <CreatePostInput />
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
