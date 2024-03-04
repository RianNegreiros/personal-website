import { useState, useEffect } from 'react'
import { getAdminPosts } from '@/app/utils/api'
import { AxiosError } from 'axios'
import { AdminPost } from '../models'
import { useLoading } from '../contexts/LoadingContext'

export const useAdminPosts = () => {
  const [data, setData] = useState<AdminPost[]>([])
  const [error, setError] = useState<AxiosError | null>(null)

  const { isLoading, setLoading } = useLoading()

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true)
      try {
        const posts = await getAdminPosts()
        setData(posts)
      } catch (error) {
        setError(error as AxiosError)
      } finally {
        setLoading(false)
      }
    }

    fetchData()
  }, [setLoading])

  return { data, isLoading, error }
}
