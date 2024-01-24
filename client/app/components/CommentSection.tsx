"use client"

import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { Comment, CommentData } from "../models";
import { addCommentToPost, getCommentsForPost, getIsUserLoggedIn } from "../utils/api";
import AuthLinks from "./AuthLinks";
import { usePathname } from "next/navigation";

interface CommentSectionProps {
  slug: string;
}

const CommentSection: React.FC<CommentSectionProps> = ({ slug }) => {
  const [comments, setComments] = useState<Comment[]>([]);
  const [formData, setFormData] = useState<CommentData>({
    postSlug: slug,
    content: '',
    token: '',
  });

  const [isLogged, setIsLogged] = useState(false);

  let pathname = usePathname()

  useEffect(() => {
    async function fetchDataAndComments() {
      const commentsData = await getCommentsForPost(slug);
      setComments(commentsData.data);
    }
    fetchDataAndComments();
  }, [slug]);

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
      console.log(formData);
      await addCommentToPost(formData);
      const response = await getCommentsForPost(slug);
      setComments(response.data);
      setFormData((prevData) => ({ ...prevData, content: '' }));
    } catch (error) {
      console.error('Failed to create comment:', error);
    }
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  return (
    <>
      <ol className="relative">
        {comments.map((comment) => (
          <li key={comment.id} className="mb-10 ml-4">
            <div className="absolute w-3 h-3 bg-gray-200 rounded-full mt-1.5 -left-1.5 border border-white dark:border-gray-900 dark:bg-gray-700"></div>
            <time className="mb-1 text-sm font-normal leading-none text-gray-400 dark:text-gray-500">
              {new Date(comment.createdAt.split('.')[0]).toLocaleDateString()}
            </time>
            <h3 className="text-lg font-semibold text-gray-900 dark:text-white">{comment.author.userName}</h3>
            <p className="mb-4 text-base font-normal text-gray-500 dark:text-gray-400">{comment.content}</p>
          </li>
        ))}
      </ol>

      <form onSubmit={handleSubmit}>
        <div className="w-full mb-4 rounded-lg bg-gray-50 dark:bg-gray-700 dark:border-gray-600">
          <div className="px-4 py-2 bg-white rounded-t-lg dark:bg-gray-800">
            <label htmlFor="content" className="sr-only">
              Seu comentário
            </label>
            <textarea
              name="content"
              id="content"
              value={formData.content}
              onChange={handleChange}
              rows={4}
              className="w-full px-0 text-sm text-gray-900 bg-white border-0 dark:bg-gray-800 focus:ring-0 dark:text-white dark:placeholder-gray-400 disabled:cursor-not-allowed"
              disabled={!isLogged}
              placeholder={isLogged ? 'Escreva um comentário...' : 'Faça login para postar um comentário'}
              required
            ></textarea>
          </div>
          <div className="flex items-center justify-between px-3 py-2 border-t dark:border-gray-600">
            {isLogged ? (
              <button
                type="submit"
                className={`inline-flex items-center py-2.5 px-4 text-xs font-medium text-center text-white bg-teal-500 rounded-lg focus:ring-4 focus:ring-teal-200 dark:focus:ring-teal-900 hover:bg-teal-800 ${!isLogged ? 'cursor-not-allowed opacity-50' : ''
                  }`}
              >
                Postar comentário
              </button>
            ) : (
              <AuthLinks pathname={pathname} />
            )}
          </div>
        </div>
      </form>
    </>
  );
};

export default CommentSection;
