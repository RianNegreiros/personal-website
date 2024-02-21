import { useState, useEffect } from 'react'
import { Post, Comment } from '@/app/models'
import { getPostBySlug, getCommentsForPost } from '@/app/utils/api'

export const usePostAndComments = (slug: string) => {
  const [isLoading, setIsLoading] = useState(true)
  const [post, setPost] = useState<Post | null>(null)
  const [comments, setComments] = useState<Comment[]>([])

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true)
      try {
        const post = await getPostBySlug(slug)
        const comments = await getCommentsForPost(slug)
        setPost(post.data)
        setComments(comments.data)
        setIsLoading(false)
      } catch (error) {
        console.error('Failed to fetch data:', error)
        setIsLoading(false)
      }
    }
    fetchData()
  }, [slug])

  return { isLoading, post, comments, setComments }
}
