import { useState, useEffect } from 'react'
import { AxiosError } from 'axios'
import { getFeed } from '@/app/utils/api'

export const useFeed = () => {
  const [isLoading, setIsLoading] = useState(true)
  const [data, setData] = useState([])
  const [error, setError] = useState<AxiosError | null>(null)

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true)
      try {
        const feed = await getFeed()
        setData(feed.data)
        setIsLoading(false)
      } catch (error) {
        setError(error as AxiosError)
        setIsLoading(false)
      }
    }
    fetchData()
  }, [])

  return { isLoading, data, error }
}
