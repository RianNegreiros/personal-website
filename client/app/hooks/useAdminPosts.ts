import { useState, useEffect } from 'react'
import { getAdminPosts } from '@/app/utils/api'
import { AxiosError } from 'axios'
import { AdminPost } from '../models'

export const useAdminPosts = () => {
  const [data, setData] = useState<AdminPost[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<AxiosError | null>(null)

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true)
      try {
        const posts = await getAdminPosts()
        setData(posts)
      } catch (error) {
        setError(error as AxiosError)
      } finally {
        setIsLoading(false)
      }
    }

    fetchData()
  }, [])

  return { data, isLoading, error }
}
