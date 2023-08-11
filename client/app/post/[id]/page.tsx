"use client"

import { Post, Comment, CommentData } from "@/app/models";
import { addCommentToPost, getCommentsForPost, getIsUserLoggedIn, getPost } from "@/app/utils/api";
import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import ReactMarkdown from "react-markdown";

async function fetchData(id: string) {
  const postData = await getPost(id);
  const commentsData = await getCommentsForPost(id);
  return { postData, commentsData };
}

export default function PostPage({ params }: { params: { id: string } }) {
  const [data, setData] = useState<Post>({
    id: '',
    title: '',
    content: '',
    createdAt: '',
    summary: '',
  });
  const [comments, setComments] = useState<Comment[]>([]);
  const [formData, setFormData] = useState<CommentData>({
    postId: params.id,
    content: '',
    token: '',
  });

  const [isLogged, setIsLogged] = useState(false);

  useEffect(() => {
    async function fetchDataAndComments() {
      const { postData, commentsData } = await fetchData(params.id);
      setData(postData);
      setComments(commentsData);
    }
    fetchDataAndComments();
  }, [params.id]);

  useEffect(() => {
    async function checkUserLoggedIn() {
      try {
        const response = await getIsUserLoggedIn();
        setIsLogged(response);
      } catch (error) {
        setIsLogged(false);
      }
    }
    
    checkUserLoggedIn();
  }, []);

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      await addCommentToPost(formData);
      const commentsData = await getCommentsForPost(params.id);
      setComments(commentsData);
      setFormData((prevData) => ({ ...prevData, content: '' }));
    } catch (error) {
      console.error('Failed to create comment:', error);
    }
  }

  const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  }

  return (
    <div className="xl:divide-y xl:divide-gray-200 xl:dark:divide-gray-700">
      <header className="pt-6 xl:pb-6">
        <div className="space-y-1 text-center">
          <div className="space-y-10">
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
            <ReactMarkdown>{data.content}</ReactMarkdown>
          </div>
        </div>
      </div>

      <ol className="relative border-l border-gray-200 dark:border-gray-700">
        {comments.map((comment) => (
          <li key={comment.id} className="mb-10 ml-4">
            <div className="absolute w-3 h-3 bg-gray-200 rounded-full mt-1.5 -left-1.5 border border-white dark:border-gray-900 dark:bg-gray-700"></div>
            <time className="mb-1 text-sm font-normal leading-none text-gray-400 dark:text-gray-500">{new Date(comment.createdAt.split('.')[0]).toLocaleDateString()}</time>
            <h3 className="text-lg font-semibold text-gray-900 dark:text-white">{comment.author.userName}</h3>
            <p className="mb-4 text-base font-normal text-gray-500 dark:text-gray-400">{comment.content}</p>
          </li>
        ))}
      </ol>

      <form onSubmit={handleSubmit}>
        <div className="w-full mb-4 border border-gray-200 rounded-lg bg-gray-50 dark:bg-gray-700 dark:border-gray-600">
          <div className="px-4 py-2 bg-white rounded-t-lg dark:bg-gray-800">
            <label
              htmlFor="comment"
              className="sr-only"
            >
              Seu comentário
            </label>
            <textarea
              name="content"
              id="content"
              value={formData.content}
              onChange={handleChange}
              rows={4}
              className="w-full px-0 text-sm text-gray-900 bg-white border-0 dark:bg-gray-800 focus:ring-0 dark:text-white dark:placeholder-gray-400"
              placeholder={isLogged ? 'Escreva um comentário...' : 'Faça o login para postar um comentário'}
              required
            ></textarea>
          </div>
          <div className="flex items-center justify-between px-3 py-2 border-t dark:border-gray-600">
          <button
            type="submit"
            className={`inline-flex items-center py-2.5 px-4 text-xs font-medium text-center text-white bg-teal-500 rounded-lg focus:ring-4 focus:ring-teal-200 dark:focus:ring-teal-900 hover:bg-teal-800 ${!isLogged ? 'cursor-not-allowed opacity-50' : ''}`}
            disabled={!isLogged}
          >
              Poste um comentário
            </button>
          </div>
        </div>
      </form>

    </div>
  )
}