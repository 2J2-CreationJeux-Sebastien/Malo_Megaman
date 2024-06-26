﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Gestion de déplacement et du saut du personnage à l'aide des touches : a, d et w      
* Gestion des détections de collision entre le personnage et les objets du jeu  
* Par: Vahik Toroussian
* Modifié: 5/12/2018
*/
public class ControlerPersonnage : MonoBehaviour
{
    public float vitesseX;      //vitesse horizontale actuelle
    public float vitesseXMax;   //vitesse horizontale Maximale désirée
    public float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut désirée
    public bool partieTerminee = false;
    public bool attaque = false;

    public GameObject Abeille;


    /* Détection des touches et modification de la vitesse de déplacement;
       "a" et "d" pour avancer et reculer, "w" pour sauter
    */
    void Update ()
    {
        if (partieTerminee == false) 
        {
            if (Input.GetKeyDown(KeyCode.Space) && attaque == false)
            {
                attaque = true;
                Invoke("AnnuleAttaque", 0.4f);
                GetComponent<Animator>().SetTrigger("attaqueAnim");
                GetComponent<Animator>().SetBool("jump", false);
            }

            if (attaque == true && vitesseX <= vitesseXMax && vitesseX >= -vitesseXMax)
            {
                vitesseX *= 4;
            }

            // déplacement vers la gauche
            if (Input.GetKey("a"))
            {
                vitesseX = -vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = true;

            }
            else if (Input.GetKey("d"))   //déplacement vers la droite
            {
                vitesseX = vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;  //mémorise vitesse actuelle en X
            }

            // sauter l'objet à l'aide la touche "w"
            if (Input.GetKeyDown("w") && Physics2D.OverlapCircle(transform.position, 0.5f))
            {
                vitesseY = vitesseSaut;
                if (partieTerminee == false) 
                {
                    GetComponent<Animator>().SetBool("jump", true);
                }      
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
            }

            //Applique les vitesses en X et Y
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


            //**************************Gestion des animaitons de course et de repos********************************
            //Active l'animation de course si la vitesse de déplacement n'est pas 0, sinon le repos sera jouer par Animator

            if (vitesseX > 0.1f || vitesseX < -0.1f)
            {
                GetComponent<Animator>().SetBool("sprint", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("sprint", false);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if (Physics2D.OverlapCircle(transform.position, 0.5f))
        {
            GetComponent<Animator>().SetBool("jump", false);
        }
        if (infoCollision.gameObject.name == "Bombe")
        {
            partieTerminee = true;
            GetComponent<Animator>().SetTrigger("death");
            Invoke("RecomencerJeu", 2f);
        }

        else if(infoCollision.gameObject.name == "Abeille" )
        {
            if (attaque == true)
            {
                Destroy(infoCollision.gameObject, 1f);
                infoCollision.gameObject.GetComponent<Animator>().SetTrigger("explosionAbeille");
                infoCollision.gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else 
            {
                partieTerminee = true;
                GetComponent<Animator>().SetTrigger("death");
                Invoke("RecomencerJeu", 2f);
            }
        }
    }

    public void AnnuleAttaque() 
    { 
        attaque = false;
    }

    void RecomencerJeu()
    {
        SceneManager.LoadScene(0);
    }
}

