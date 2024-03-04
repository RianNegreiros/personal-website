import { useState, useEffect } from 'react'
import { Post, Comment } from '@/app/models'
import { getPostBySlug, getCommentsForPost } from '@/app/utils/api'
import { useLoading } from '../contexts/LoadingContext'

export const usePostAndComments = (slug: string) => {
  const [post, setPost] = useState<Post | null>(null)
  const [comments, setComments] = useState<Comment[]>([])

  const { isLoading, setLoading } = useLoading()

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true)
      try {
        const post = await getPostBySlug(slug)
        const comments = await getCommentsForPost(slug)
        setPost(post.data)
        setComments(comments.data)
        setLoading(false)
      } catch (error) {
        console.error('Failed to fetch data:', error)
        setLoading(false)
      }
    }
    fetchData()
  }, [setLoading, slug])

  return { isLoading, post, comments, setComments }
}
