'use client'

import { useState, useEffect } from 'react'
import { getProjects } from '@/app/utils/api'
import { Project } from '../models'
import { useLoading } from '../contexts/LoadingContext';

export function useProjects() {
  const { isLoading, setLoading } = useLoading();
  const [projects, setProjects] = useState<Project[]>([])

  useEffect(() => {
    getProjects()
      .then((projects) => {
        setProjects(projects.data)
        setLoading(false)
      })
      .catch((error) => {
        console.error(error)
        setLoading(false)
      })
  }, [setLoading])

  return { isLoading, projects }
}
