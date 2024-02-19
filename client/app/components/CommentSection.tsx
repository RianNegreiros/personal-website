import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { Comment, CommentData, InputReply } from "../models";
import { addCommentToPost, addReplyToComment, getCommentsForPost } from "../utils/api";
import { useAuth } from "../contexts/AuthContext";

interface CommentSectionProps {
  slug: string;
  comments: Comment[];
  setComments: React.Dispatch<React.SetStateAction<Comment[]>>;
}

const CommentSection = ({ slug, comments, setComments }: CommentSectionProps) => {
  const { isLogged } = useAuth();
  const [formData, setFormData] = useState<CommentData>({
    postSlug: slug,
    content: '',
    authorId: ''
  });
  const [replyData, setReplyData] = useState<InputReply>({
    id: '',
    content: '',
    authorId: ''
  });
  const [replyingTo, setReplyingTo] = useState<string | null>(null);
  const [showReplyForm, setShowReplyForm] = useState<boolean>(false);
  const [isCommentsVisible, setIsCommentsVisible] = useState(false);

  const handleReplyClick = (commentId: string) => {
    setReplyingTo(commentId);
    setShowReplyForm(!showReplyForm);
  };

  useEffect(() => {
    setComments(comments);
  }, [comments, setComments]);

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
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

  const handleReplySubmit = async (e: FormEvent<HTMLFormElement>, commentId: string) => {
    e.preventDefault();
    try {
      await addReplyToComment(commentId, replyData);
      const response = await getCommentsForPost(slug);
      setComments(response.data);
      setReplyData((prevData: InputReply) => ({ ...prevData, content: '' }));
      setReplyingTo(null)
    } catch (error) {
      console.error('Failed to create reply:', error);
    }
  };

  const handleReplyChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setReplyData((prevData: InputReply) => ({ ...prevData, [name]: value }));
  };

  return (
    <main className="bg-white dark:bg-gray-900">
      <div className="px-4 mx-auto max-w-screen-xl ">
        <article className="mx-auto w-full max-w-2xl">
          <button
            onClick={() => setIsCommentsVisible(!isCommentsVisible)}
            className="justify-between mb-6 text-white bg-dracula-pink hover:bg-dracula-pink-800 focus:ring-4 focus:outline-none focus:ring-dracula-pink-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center dark:bg-dracula-pink-400 dark:hover:bg-dracula-pink dark:focus:ring-dracula-pink"
            type="button">
            <h2 className="text-lg font-bold text-gray-900 dark:text-white">Comentários ({comments.length})</h2>
            <svg className="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
              <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m1 1 4 4 4-4" />
            </svg>
          </button>
          <section
            className={`transition-transform duration-500 ease-in-out transform ${isCommentsVisible ? '' : 'hidden'}`}
          >
            <form className="mb-6" onSubmit={handleSubmit}>
              <div className="py-2 px-4 mb-4 bg-white rounded-lg rounded-t-lg border border-gray-200 dark:bg-gray-800 dark:border-gray-700">
                <label htmlFor="comment" className="sr-only">Seu comentário</label>
                <textarea
                  id="comment"
                  name="content"
                  rows={6}
                  disabled={!isLogged}
                  placeholder={isLogged ? 'Escreva um comentário...' : 'Faça login para postar um comentário'}
                  className="px-0 w-full text-sm text-gray-900 border-0 focus:ring-0 dark:text-white dark:placeholder-gray-400 dark:bg-gray-800"
                  required onChange={handleChange} value={formData.content}></textarea>
              </div>
              <button type="submit"
                className={`inline-flex items-center py-2.5 px-4 text-xs font-medium text-center text-white bg-dracula-pink rounded-lg focus:ring-4 focus:ring-dracula-pink-200 dark:focus:ring-dracula-pink-900 hover:bg-dracula-pink-800 ${!isLogged ? 'cursor-not-allowed opacity-50' : ''}`}
              >
                Postar comentário
              </button>
            </form>
            {comments.map((comment) => (
              <article key={comment.id} className="p-6 text-base bg-white rounded-lg dark:bg-gray-900">
                <footer className="flex justify-between items-center mb-2">
                  <div className="flex items-center">
                    <p className="inline-flex items-center mr-3 font-semibold text-sm text-gray-900 dark:text-white">{comment.authorUsername}</p>
                    <p className="text-sm text-gray-600 dark:text-gray-400"><time dateTime={comment.createdAt}
                      title={new Date(comment.createdAt).toLocaleDateString()}>{new Date(comment.createdAt).toLocaleDateString()}</time></p>
                  </div>
                </footer>
                <p>{comment.content}</p>
                <div className="flex items-center mt-4 space-x-4">
                  <button type="button"
                    className={`flex items-center font-medium text-sm text-gray-500 hover:underline dark:text-gray-400 ${!isLogged ? 'cursor-not-allowed opacity-50' : ''}`}
                    onClick={() => handleReplyClick(comment.id)}
                    disabled={!isLogged}
                  >
                    <svg className="mr-1.5 w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 18">
                      <path d="M18 0H2a2 2 0 0 0-2 2v9a2 2 0 0 0 2 2h2v4a1 1 0 0 0 1.707.707L10.414 13H18a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2Zm-5 4h2a1 1 0 1 1 0 2h-2a1 1 0 1 1 0-2ZM5 4h5a1 1 0 1 1 0 2H5a1 1 0 0 1 0-2Zm2 5H5a1 1 0 0 1 0-2h2a1 1 0 0 1 0 2Zm9 0h-6a1 1 0 0 1 0-2h6a1 1 0 1 1 0 2Z" />
                    </svg>
                    Reply
                  </button>
                </div>
                {replyingTo === comment.id && showReplyForm && (
                  <form onSubmit={(e) => handleReplySubmit(e, comment.id)} className="mt-4">
                    <input
                      type="text"
                      name="content"
                      value={replyData.content}
                      onChange={handleReplyChange}
                      className="p-2 w-full border rounded-md"
                    />
                    <button type="submit" className="mt-2 px-4 py-2 bg-dracula-pink text-white rounded-md">Reply</button>
                  </form>
                )}
                {comment.replies && comment.replies.map((reply) => (
                  <article key={reply.id} className="p-6 ml-6 lg:ml-12 text-base bg-white rounded-lg dark:bg-gray-900">
                    <footer className="flex justify-between items-center mb-2">
                      <div className="flex items-center">
                        <p className="inline-flex items-center mr-3 font-semibold text-sm text-gray-900 dark:text-white">{reply.authorUsername}</p>
                        <p className="text-sm text-gray-600 dark:text-gray-400"><time dateTime={reply.createdAt}
                          title={new Date(reply.createdAt).toLocaleDateString()}>{new Date(reply.createdAt).toLocaleDateString()}</time></p>
                      </div>
                    </footer>
                    <p>{reply.content}</p>
                    <div className="flex items-center mt-4 space-x-4">
                      <button type="button"
                        className={`flex items-center font-medium text-sm text-gray-500 hover:underline dark:text-gray-400 ${!isLogged ? 'cursor-not-allowed opacity-50' : ''}`}
                        onClick={() => handleReplyClick(reply.id)}
                        disabled={!isLogged}
                      >
                        <svg className="mr-1.5 w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 18">
                          <path d="M18 0H2a2 2 0 0 0-2 2v9a2 2 0 0 0 2 2h2v4a1 1 0 0 0 1.707.707L10.414 13H18a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2Zm-5 4h2a1 1 0 1 1 0 2h-2a1 1 0 1 1 0-2ZM5 4h5a1 1 0 1 1 0 2H5a1 1 0 0 1 0-2Zm2 5H5a1 1 0 0 1 0-2h2a1 1 0 0 1 0 2Zm9 0h-6a1 1 0 0 1 0-2h6a1 1 0 1 1 0 2Z" />
                        </svg>
                        Reply
                      </button>
                    </div>
                    {replyingTo === reply.id && showReplyForm && (
                      <form onSubmit={(e) => handleReplySubmit(e, comment.id)}>
                        <input
                          type="text"
                          name="content"
                          value={replyData.content}
                          onChange={handleReplyChange}
                          className="p-2 w-full border rounded-md"
                        />
                        <button type="submit" className="mt-2 px-4 py-2 bg-dracula-pink text-white rounded-md">Reply</button>
                      </form>
                    )}
                  </article>
                ))}
              </article>
            ))}
          </section>
        </article>
      </div>
    </main >
  );
};

export default CommentSection;