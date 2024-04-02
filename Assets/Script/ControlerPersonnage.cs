using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /* Détection des touches et modification de la vitesse de déplacement;
       "a" et "d" pour avancer et reculer, "w" pour sauter
    */
    void Update ()
    {
        // déplacement vers la gauche
        if (Input.GetKey("a"))
        {
            vitesseX = -vitesseXMax;

        }
        else if (Input.GetKey("d"))   //déplacement vers la droite
        {
            vitesseX = vitesseXMax;
        }
        else
        {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;  //mémorise vitesse actuelle en X
        }

        // sauter l'objet à l'aide la touche "w"
        if (Input.GetKeyDown("w"))
        {
            vitesseY = vitesseSaut;
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
        }
        
        //Applique les vitesses en X et Y
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


        //**************************Gestion des animaitons de course et de repos********************************
        //Active l'animation de course si la vitesse de déplacement n'est pas 0, sinon le repos sera jouer par Animator

        if (vitesseX > 0.1f || vitesseX < 0.1f) {
            GetComponent<Animator>().SetBool("Sprint", true);
        } else {
            GetComponent<Animator>().SetBool("Sprint", true);
        }
      
       
       


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    
      
    }
}

