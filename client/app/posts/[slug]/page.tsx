"use client"

import CommentSection from "@/app/components/CommentSection";
import Loading from "@/app/components/Loading";
import { Post, Comment } from "@/app/models";
import { getCommentsForPost, getPostBySlug } from "@/app/utils/api";
import siteMetadata from "@/app/utils/siteMetaData";
import { EmailIcon, EmailShareButton, LinkedinIcon, LinkedinShareButton, PocketIcon, PocketShareButton } from "next-share";
import { useEffect, useState } from "react";
import ReactMarkdown from "react-markdown";
import rehypeRaw from "rehype-raw";
import remarkGfm from "remark-gfm";

interface PostPageProps {
  params: { slug: string };
}

async function fetchData(slug: string) {
  const postData = await getPostBySlug(slug);
  const commentsData = await getCommentsForPost(slug);
  return { postData, commentsData };
}

export default function PostPage({ params }: { params: { slug: string } }) {
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<Post>({
    id: '',
    title: '',
    summary: '',
    content: '',
    slug: '',
    createdAt: ''
  });
  const [comments, setComments] = useState<Comment[]>([]);

  useEffect(() => {
    async function fetchDataAndComments() {
      const { postData, commentsData } = await fetchData(params.slug);
      setData(postData.data);
      setComments(commentsData.data);
      setIsLoading(false);
    }
    fetchDataAndComments();
  }, [params.slug]);

  return (
    <>
      {isLoading ? (
        <Loading />
      ) : (
        <>
          <header className="pt-6 xl:pb-6">

            <div className="space-y-1 text-center">
              <div className="space-y-10 mb-3">
                <div>
                  <p className="text-base font-medium leading-6 text-teal-500">
                    {new Date(data.createdAt.split('.')[0]).toLocaleDateString()}
                  </p>
                </div>
              </div>

              <div>
                <h1 className="text-3xl font-extrabold leading-9 tracking-tight text-gray-900 dark:text-gray-100 sm:text-4xl sm:leading-10 md:text-5xl md:leading-14">
                  {data.title}
                </h1>
              </div>
            </div>
          </header>

          <div className="divide-y divide-gray-200 pb-7 dark:divide-gray-700 xl:divide-y-0">
            <div className="divide-y divide-gray-200 dark:divide-gray-700 xl:col-span-3 xl:row-span-2 xl:pb-0">
              <div className="prose max-w-none pb-8 pt-10 dark:prose-invert prose-lg">
                <ReactMarkdown rehypePlugins={[remarkGfm, rehypeRaw]}>{data.content}</ReactMarkdown>
              </div>
            </div>
          </div>

          <div className="flex justify-center space-x-4 mb-4">
            <LinkedinShareButton
              url={`${siteMetadata.siteUrl}/posts/${data.slug}`}
              title={data.title}
              about={data.summary}
            >
              <LinkedinIcon size={32} round />
            </LinkedinShareButton>

            <PocketShareButton
              url={`${siteMetadata.siteUrl}/posts/${data.slug}`}
              title={data.title}
              about={data.summary}
            >
              <PocketIcon size={32} round />
            </PocketShareButton>

            <EmailShareButton
              url={`${siteMetadata.siteUrl}/posts/${data.slug}`}
              subject={data.title}
              about={data.summary}
              body="body"
            >
              <EmailIcon size={32} round />
            </EmailShareButton>
          </div>

          <CommentSection slug={params.slug} />
        </>
      )}
    </>
  )
}
